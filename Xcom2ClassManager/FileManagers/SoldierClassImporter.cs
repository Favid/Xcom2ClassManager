using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xcom2ClassManager.DataObjects;

namespace Xcom2ClassManager.FileManagers
{
    public class SoldierClassImporter
    {
        private struct MissingAbilityEntry
        {
            public string className;
            public string abilityName;
            public WeaponSlot weaponSlot;
            public SoldierRank rank;
            public int abilitySlot;
        }

        private List<SoldierClass> soldierClasses;
        private List<MissingAbilityEntry> missingAbilities;
        private string intFile;
        private string classFile;
        private string gameFile;

        public SoldierClassImporter()
        {

        }

        public List<SoldierClass> importSoldierClasses(string intFile, string classFile, string gameFile)
        {
            soldierClasses = new List<SoldierClass>();
            missingAbilities = new List<MissingAbilityEntry>();
            this.intFile = intFile;
            this.classFile = classFile;
            this.gameFile = gameFile;

            importClassFile();
            importIntFile();
            importGameFile();

            padSoldierStats();
            padSoldierAbilities();

            return soldierClasses;
        }

        private void padSoldierStats()
        {
            foreach (SoldierClass soldierClass in soldierClasses)
            {
                List<SoldierClassStat> newStats = new List<SoldierClassStat>();

                foreach (Stat stat in Enum.GetValues(typeof(Stat)))
                {
                    foreach (SoldierRank rank in Enum.GetValues(typeof(SoldierRank)))
                    {
                        if (rank != SoldierRank.Rookie)
                        {
                            SoldierClassStat newStat = soldierClass.stats.Where(x => x.stat == stat && x.rank == rank).SingleOrDefault();
                            if (newStat == null)
                            {
                                newStat = new SoldierClassStat();
                                newStat.rank = rank;
                                newStat.stat = stat;
                                newStat.value = null;
                            }

                            newStats.Add(newStat);
                        }
                    }
                }

                soldierClass.stats = new List<SoldierClassStat>(newStats);
            }
        }

        private void padSoldierAbilities()
        {
            foreach (SoldierClass soldierClass in soldierClasses)
            {
                List<SoldierClassAbility> newAbilities = new List<SoldierClassAbility>();
                
                for (int i = 1; i <= 6; i++)
                {
                    SoldierClassAbility newAbility = soldierClass.soldierAbilities.Where(x => x.slot == i && x.rank == SoldierRank.Squaddie).SingleOrDefault();
                    if (newAbility == null)
                    {
                        newAbility = new SoldierClassAbility();
                        newAbility.rank = SoldierRank.Squaddie;
                        newAbility.slot = i;
                    }

                    newAbilities.Add(newAbility);
                }

                foreach (SoldierRank rank in Enum.GetValues(typeof(SoldierRank)))
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        if (rank > SoldierRank.Squaddie)
                        {
                            SoldierClassAbility newAbility = soldierClass.soldierAbilities.Where(x => x.slot == i && x.rank == rank).SingleOrDefault();
                            if (newAbility == null)
                            {
                                newAbility = new SoldierClassAbility();
                                newAbility.rank = rank;
                                newAbility.slot = i;
                            }

                            newAbilities.Add(newAbility);
                        }
                    }
                }

                soldierClass.soldierAbilities = new List<SoldierClassAbility>(newAbilities);
            }
        }

        private void importClassFile()
        {
            StreamReader file = new StreamReader(classFile);
            string line;

            SoldierClass readingClass = null;
            SoldierRank readingRank = SoldierRank.Rookie;
            int readingAbilitySlot = 0;

            bool unsaved = true;

            List<string> classNames = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("SoldierClasses=") && !line.Contains("-") && !line.Contains("."))
                {
                    int openQuote = line.IndexOf("\"");
                    int closeQuote = line.LastIndexOf("\"");
                    classNames.Add(line.Substring(openQuote + 1, closeQuote - openQuote - 1));
                }

                if (line.Contains("X2SoldierClassTemplate]"))
                {
                    int space = line.IndexOf(" ");
                    string className = line.Substring(1, space - 1);

                    if (unsaved && readingClass != null)
                    {
                        unsaved = false;
                        soldierClasses.Add(readingClass);
                        readingClass = null;
                    }

                    if (classNames.Contains(className))
                    {
                        readingClass = new SoldierClass();
                        readingClass.metadata.internalName = className;

                        readingRank = SoldierRank.Rookie;

                        unsaved = true;
                    }
                }

                if (readingClass != null)
                {
                    if (line.Contains("IconImage"))
                    {
                        readingClass.metadata.iconString = getStringValue(line);
                    }

                    if (line.Contains("NumInForcedDeck"))
                    {
                        readingClass.experience.numberInForcedDeck = getIntValue(line);
                    }

                    if (line.Contains("NumInDeck"))
                    {
                        readingClass.experience.numberInDeck = getIntValue(line);
                    }

                    if (line.Contains("KillAssistsPerKill"))
                    {
                        readingClass.experience.killAssistsPerKill = getIntValue(line);
                    }

                    if (line.Contains("SquaddieLoadout"))
                    {
                        readingClass.equipment.squaddieLoadout = getStringValue(line);
                    }

                    if (line.Contains("AllowedWeapons"))
                    {
                        int secondAssign = findStartingIndexOfNthOccurrence(line, "=", 2, 0);
                        int thirdAssign = findStartingIndexOfNthOccurrence(line, "=", 3, 0);
                        int commaIndex = findStartingIndexOfNthOccurrence(line, ",", 1, 0);

                        if (secondAssign > -1 && thirdAssign > -1 && commaIndex > -1)
                        {
                            string slotStringValue = line.Substring(secondAssign + 1, commaIndex - secondAssign - 1);
                            string weaponStringValue = line.Substring(thirdAssign + 2, line.Length - thirdAssign - 4); // +2 for quote, -2 for quote and paren

                            WeaponSlot weaponSlot = Enums.GetValueFromDescription<WeaponSlot>(slotStringValue);
                            Weapon weapon = new Weapon(weaponStringValue, weaponSlot);
                            readingClass.equipment.weapons.Add(weapon);
                        }
                    }

                    if (line.Contains("AllowedArmors"))
                    {
                        readingClass.equipment.allowedArmors.Add(getStringValue(line));
                    }

                    if (line.Contains("bAllowAWCAbilities"))
                    {
                        readingClass.allowAwcAbilities = (getIntValue(line) == 1 || getStringValue(line) == "true");
                    }

                    if (line.Contains("ExcludedAbilities"))
                    {
                        string abilityName = getStringValue(line);
                        Ability ability = ProjectState.getAbility(abilityName);
                        if (ability != null)
                        {
                            readingClass.awcExcludeAbilities.Add(ability);
                        }
                    }

                    if (line.Contains("bCanHaveBonds"))
                    {
                        readingClass.metadata.allowBonds = (getIntValue(line) == 1 || getStringValue(line) == "true");
                    }

                    if (line.Contains("UnfavoredClasses"))
                    {
                        readingClass.metadata.unfavoredClasses.Add(getStringValue(line));
                    }

                    if (line.Contains("BaseAbilityPointsPerPromotion"))
                    {
                        readingClass.baseAbilityPointsPerPromotion = getIntValue(line);
                    }

                    // Ranks

                    if (line.Contains("SoldierRanks"))
                    {
                        readingRank = readingRank + 1;
                        readingAbilitySlot = 1;
                    }

                    if (readingRank > SoldierRank.Rookie && readingRank <= SoldierRank.Brigadier && readingAbilitySlot > 0 && readingAbilitySlot < 7)
                    {
                        int counter = 0;
                        int numAbilityName = Regex.Matches(line, "AbilityName").Count;
                        while (counter < numAbilityName)
                        {
                            counter++;
                            int tagStartIndex = findStartingIndexOfNthOccurrence(line, "AbilityName", counter, 0);
                            int startingIndex = findStartingIndexOfNthOccurrence(line, "\"", 1, tagStartIndex) + 1;
                            int endingIndex = findStartingIndexOfNthOccurrence(line, "\"", 2, tagStartIndex);

                            string abilityName = line.Substring(startingIndex, endingIndex - startingIndex);
                            WeaponSlot slot = WeaponSlot.None;

                            tagStartIndex = findStartingIndexOfNthOccurrence(line, "ApplyToWeaponSlot", counter, 0);
                            if (tagStartIndex > -1)
                            {
                                startingIndex = findStartingIndexOfNthOccurrence(line, "eInvSlot", 1, tagStartIndex);

                                // either a space or ) can signify the end
                                int endingParenIndex = findStartingIndexOfNthOccurrence(line, ")", 1, tagStartIndex);
                                int endingSpaceIndex = findStartingIndexOfNthOccurrence(line, " ", 1, tagStartIndex);

                                if (endingParenIndex != -1 && endingSpaceIndex != -1)
                                {
                                    endingIndex = Math.Min(endingParenIndex, endingSpaceIndex);
                                }
                                else if (endingParenIndex == -1)
                                {
                                    endingIndex = endingSpaceIndex;
                                }
                                else if (endingSpaceIndex == -1)
                                {
                                    endingIndex = endingParenIndex;
                                }

                                if (endingIndex != -1)
                                {
                                    string slotString = line.Substring(startingIndex, endingIndex - startingIndex);
                                    slot = Enums.GetValueFromDescription<WeaponSlot>(slotString);
                                }

                            }

                            Ability ability = ProjectState.getClosestMatchingAbility(abilityName, slot);

                            if (ability != null)
                            {
                                SoldierClassAbility soldierAbility = new SoldierClassAbility(ProjectState.getClosestMatchingAbility(abilityName, slot));
                                soldierAbility.rank = readingRank;
                                soldierAbility.slot = readingAbilitySlot;

                                readingClass.soldierAbilities.Add(soldierAbility);
                            }
                            else
                            {
                                MissingAbilityEntry soldierAbility = new MissingAbilityEntry();
                                soldierAbility.className = readingClass.metadata.internalName;
                                soldierAbility.abilityName = abilityName;
                                soldierAbility.weaponSlot = slot;
                                soldierAbility.rank = readingRank;
                                soldierAbility.abilitySlot = readingAbilitySlot;
                                missingAbilities.Add(soldierAbility);
                            }

                            readingAbilitySlot++;
                        }
                        
                        counter = 0;
                        int numStatType = Regex.Matches(line, "StatType").Count;
                        while (counter < numStatType)
                        {
                            counter++;
                            int tagStartIndex = findStartingIndexOfNthOccurrence(line, "StatType", counter, 0);
                            if (tagStartIndex > -1)
                            {
                                Stat? statType = null;

                                int startingIndex = findStartingIndexOfNthOccurrence(line, "eStat", 1, tagStartIndex);
                                int endingIndex = findStartingIndexOfNthOccurrence(line, ",", 1, startingIndex);

                                string statTypeString = line.Substring(startingIndex, endingIndex - startingIndex);
                                if (statTypeString != "eStat_CombatSims") // TODO handle this
                                {
                                    statType = Enums.GetValueFromDescription<Stat>(statTypeString);
                                }

                                startingIndex = findStartingIndexOfNthOccurrence(line, "=", 1, endingIndex) + 1;
                                endingIndex = findStartingIndexOfNthOccurrence(line, ")", 1, startingIndex);

                                string statAmountString = line.Substring(startingIndex, endingIndex - startingIndex).Trim();

                                if (int.TryParse(statAmountString, out int statAmount) && statType.HasValue)
                                {
                                    SoldierClassStat soldierStat = new SoldierClassStat();
                                    soldierStat.rank = readingRank;
                                    soldierStat.stat = statType.Value;
                                    soldierStat.value = statAmount;

                                    readingClass.stats.Add(soldierStat);
                                }
                            }
                        }
                    }
                }
            }

            if (unsaved)
            {
                soldierClasses.Add(readingClass);
            }
        }

        private void importIntFile()
        {
            StreamReader file = new StreamReader(intFile);
            string line;
            
            Ability foundAbility = null;
            List<Ability> foundAbilities = new List<Ability>();
            List<Ability> allAbilities = ProjectState.getAbilities();

            SoldierClass foundClass = null;
            List<SoldierClass> foundClasses = new List<SoldierClass>();

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("["))
                {
                    if (foundAbility != null)
                    {
                        foundAbilities.Add(foundAbility);
                        foundAbility = null;
                    }

                    if (line.Contains(" X2AbilityTemplate]"))
                    {
                        string abilityName = line.Substring(1, line.IndexOf(' ') - 1);
                        // TODO Kind of a hack - Ignores abilities whose names already exist
                        // Will probably want an overwrite option in the future
                        if (foundAbilities.Where(x => x.internalName.Equals(abilityName, StringComparison.OrdinalIgnoreCase)).Count() == 0
                            && allAbilities.Where(x => x.internalName.Equals(abilityName, StringComparison.OrdinalIgnoreCase)).Count() == 0)
                        {
                            foundAbility = new Ability();
                            foundAbility.internalName = abilityName;
                        }
                    }

                    if (foundClass != null)
                    {
                        foundClasses.Add(foundClass);
                        foundClass = null;
                    }

                    if (line.Contains(" X2SoldierClassTemplate]"))
                    {
                        foundClass = new SoldierClass();
                        foundClass.metadata.internalName = line.Substring(1, line.IndexOf(' ') - 1);
                    }
                }
                else if(foundAbility != null)
                {
                    int startIndex = line.IndexOf('"') + 1;
                    int endIndex = line.LastIndexOf('"');

                    if (line.Contains("LocFriendlyName="))
                    {
                        foundAbility.displayName = line.Substring(startIndex, endIndex - startIndex);
                    }
                    else if (line.Contains("LocLongDescription="))
                    {
                        foundAbility.description = line.Substring(startIndex, endIndex - startIndex);
                    }
                }
                else if (foundClass != null)
                {
                    int startIndex = line.IndexOf('"') + 1;
                    int endIndex = line.LastIndexOf('"');

                    if (startIndex <= 0)
                    {
                        startIndex = line.IndexOf('=') + 1;
                    }

                    if (endIndex <= startIndex)
                    {
                        endIndex = line.Length;
                    }

                    if (line.Contains("DisplayName="))
                    {
                        foundClass.metadata.displayName = line.Substring(startIndex, endIndex - startIndex);
                    }
                    else if (line.Contains("ClassSummary="))
                    {
                        foundClass.metadata.description = line.Substring(startIndex, endIndex - startIndex);
                    }
                    else if (line.Contains("LeftAbilityTreeTitle=") || line.Contains("AbilityTreeTitles[0]"))
                    {
                        foundClass.leftTreeName = line.Substring(startIndex, endIndex - startIndex);
                    }
                    else if (line.Contains("RightAbilityTreeTitle=") || line.Contains("AbilityTreeTitles[2]"))
                    {
                        foundClass.rightTreeName = line.Substring(startIndex, endIndex - startIndex);
                    }
                    else if (line.Contains("RandomNickNames["))
                    {
                        foundClass.nicknames.Add(new ClassNickname(line.Substring(startIndex, endIndex - startIndex), NicknameGender.Unisex));
                    }
                    else if (line.Contains("RandomNickNames_Male"))
                    {
                        foundClass.nicknames.Add(new ClassNickname(line.Substring(startIndex, endIndex - startIndex), NicknameGender.Male));
                    }
                    else if (line.Contains("RandomNicknames_Female"))
                    {
                        foundClass.nicknames.Add(new ClassNickname(line.Substring(startIndex, endIndex - startIndex), NicknameGender.Female));
                    }
                }
            }
            
            if (foundAbility != null)
            {
                foundAbilities.Add(foundAbility);
                foundAbility = null;
            }

            if (foundClass != null)
            {
                foundClasses.Add(foundClass);
                foundClass = null;
            }

            // insert the new abilities that are in both the int and ini files
            int abilityIdIncrementor = 0;
            foreach (Ability ability in foundAbilities)
            {
                List<MissingAbilityEntry> matchingMissingAbilities = missingAbilities.Where(x => x.abilityName.Equals(ability.internalName, StringComparison.OrdinalIgnoreCase)).ToList();
                if (matchingMissingAbilities.Any())
                {
                    ability.id = ProjectState.getNextAbilityId() + abilityIdIncrementor;
                    ability.weaponSlot = matchingMissingAbilities.First().weaponSlot;
                    ability.requiredMod = "Class Import";
                    abilityIdIncrementor++;

                    foreach (MissingAbilityEntry missingAbility in matchingMissingAbilities)
                    {
                        SoldierClassAbility soldierAbility = new SoldierClassAbility(ability);
                        soldierAbility.rank = missingAbility.rank;
                        soldierAbility.slot = missingAbility.abilitySlot;

                        SoldierClass classToUpdate = soldierClasses.Where(x => x.metadata.internalName == missingAbility.className).FirstOrDefault();
                        classToUpdate.soldierAbilities.Add(soldierAbility);
                    }
                }
            }

            foreach(SoldierClass soldierClass in foundClasses)
            {
                SoldierClass classToUpdate = soldierClasses.Where(x => x.metadata.internalName == soldierClass.metadata.internalName).FirstOrDefault();

                if (classToUpdate != null)
                {
                    classToUpdate.metadata.displayName = soldierClass.metadata.displayName;
                    classToUpdate.metadata.description = soldierClass.metadata.description;
                    classToUpdate.leftTreeName = soldierClass.leftTreeName;
                    classToUpdate.rightTreeName = soldierClass.rightTreeName;
                    classToUpdate.nicknames.AddRange(soldierClass.nicknames);
                }
            }

            // TODO this probably shouldn't be done yet
            AbilityManager abilityManager = new AbilityManager();
            abilityManager.addAbilities(foundAbilities.Where(x => x.requiredMod == "Class Import").ToList());
            ProjectState.reloadAbilities();
        }

        private void importGameFile()
        {
            StreamReader file = new StreamReader(gameFile);
            string line;

            bool inLoadoutSection = false;
            
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("["))
                {
                    inLoadoutSection = line.Contains("[XComGame.X2ItemTemplateManager]");
                }
                else if (inLoadoutSection)
                {
                    if (line.Contains("LoadoutName"))
                    {
                        string className = getNextStringValue(line, line.IndexOf("LoadoutName"));
                        SoldierClass soldierClass = soldierClasses.Where(x => x.metadata.internalName == className).FirstOrDefault();
                        if (soldierClass != null)
                        {
                            int itemsIndex = 0;
                            string loadoutItem = string.Empty;
                            do
                            {
                                loadoutItem = getLoadoutItem(line, itemsIndex);
                                if (loadoutItem != string.Empty)
                                {
                                    soldierClass.loadoutItems.Add(loadoutItem);
                                }

                                itemsIndex++;
                            } while (loadoutItem != string.Empty);
                        }
                    }
                }
                
            }
        }

        private string getLoadoutItem(string line, int itemsIndex)
        {
            string value = string.Empty;
            string itemString = string.Format("Items[{0}]", itemsIndex.ToString());

            if (line.Contains(itemString))
            {
                int startIndex = findStartingIndexOfNthOccurrence(line, itemString, 1, 0);

                // find the second equals after this for the item name
                startIndex = findStartingIndexOfNthOccurrence(line, "=", 2, startIndex);

                value = getNextStringValue(line, startIndex);
            }

            return value;
        }

        private string getNextStringValue(string line, int startIndex)
        {
            string value = string.Empty;

            int valueStartIndex = -1;
            int valueEndIndex = -1;

            int assignIndex = findStartingIndexOfNthOccurrence(line, "=", 1, startIndex);
            if (assignIndex != -1)
            {
                int index = assignIndex + 1;
                while (valueStartIndex == -1 && index < line.Length)
                {
                    if (line[index] != ' ' && line[index] != '\"')
                    {
                        valueStartIndex = index;
                    }
                    else
                    {
                        index++;
                    }
                }

                if (valueStartIndex != -1)
                {
                    index = valueStartIndex + 1;
                    while (valueEndIndex == -1 && index < line.Length)
                    {
                        if (line[index] == ' ' || line[index] == '\"' || line[index] == ')')
                        {
                            valueEndIndex = index;
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
            }

            if (valueStartIndex != -1 && valueEndIndex != -1)
            {
                value = line.Substring(valueStartIndex, valueEndIndex - valueStartIndex);
            }

            return value;
        }

        private int findStartingIndexOfNthOccurrence(string line, string substring, int n, int startIndex)
        {
            int numFound = 0;

            for(int i = startIndex; i < line.Length - substring.Length; i++)
            {
                string check = line.Substring(i, substring.Length);
                if (check == substring)
                {
                    numFound++;

                    if (n == numFound)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private string getStringValue(string line)
        {
            int assign = line.IndexOf("=");
            if (line.Contains("\""))
            {
                assign++;
            }

            int end = line.Length - 1;
            if (line.Contains("\""))
            {
                end--;
            }

            string stringValue = line.Substring(assign + 1, end - assign);
            return stringValue;
        }

        private int getIntValue(string line)
        {
            int assign = line.IndexOf("=");
            int end = line.Length - 1;

            string stringValue = line.Substring(assign + 1, end - assign);
            int.TryParse(stringValue, out int intValue);

            return intValue;
        }


    }
}
