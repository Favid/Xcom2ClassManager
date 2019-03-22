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

        private List<char> classDataDelimitters;
        private List<char> intDelimitters;

        private List<SoldierClass> soldierClasses;
        private List<MissingAbilityEntry> missingAbilities;
        private string intFile;
        private string classFile;
        private string gameFile;

        public SoldierClassImporter()
        {
            classDataDelimitters = new List<char>();
            classDataDelimitters.Add(' ');
            classDataDelimitters.Add('\"');
            classDataDelimitters.Add(')');
            classDataDelimitters.Add(',');

            intDelimitters = new List<char>();
            intDelimitters.Add('\"');
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
                if (line.ToLower().Contains("SoldierClasses".ToLower()) && !line.Contains("-") && !line.Contains("."))
                {
                    string className = getNextStringValue(line, 0, classDataDelimitters);
                    classNames.Add(className);
                }

                if (line.ToLower().Contains("X2SoldierClassTemplate]".ToLower()))
                {
                    int spaceIndex = line.IndexOf(" ");
                    string className = line.Substring(1, spaceIndex - 1);

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
                    if (line.ToLower().Contains("IconImage".ToLower()))
                    {
                        readingClass.metadata.iconString = getNextStringValue(line, 0, classDataDelimitters);
                    }

                    if (line.ToLower().Contains("NumInForcedDeck".ToLower()))
                    {
                        readingClass.experience.numberInForcedDeck = getIntValue(line);
                    }

                    if (line.ToLower().Contains("NumInDeck".ToLower()))
                    {
                        readingClass.experience.numberInDeck = getIntValue(line);
                    }

                    if (line.ToLower().Contains("KillAssistsPerKill".ToLower()))
                    {
                        readingClass.experience.killAssistsPerKill = getIntValue(line);
                    }

                    if (line.ToLower().Contains("SquaddieLoadout".ToLower()))
                    {
                        readingClass.equipment.squaddieLoadout = getNextStringValue(line, 0, classDataDelimitters);
                    }

                    if (line.ToLower().Contains("AllowedWeapons".ToLower()))
                    {
                        int secondAssign = findStartingIndexOfNthOccurrence(line, "=", 2, 0);
                        int thirdAssign = findStartingIndexOfNthOccurrence(line, "=", 3, 0);

                        if (secondAssign > -1 && thirdAssign > -1)
                        {
                            string slotStringValue = getNextStringValue(line, secondAssign, classDataDelimitters);
                            string weaponStringValue = getNextStringValue(line, thirdAssign, classDataDelimitters);

                            WeaponSlot weaponSlot = Enums.GetValueFromDescription<WeaponSlot>(slotStringValue);
                            Weapon weapon = new Weapon(weaponStringValue, weaponSlot);
                            readingClass.equipment.weapons.Add(weapon);
                        }
                    }

                    if (line.ToLower().Contains("AllowedArmors".ToLower()))
                    {
                        readingClass.equipment.allowedArmors.Add(getNextStringValue(line, 0, classDataDelimitters));
                    }

                    if (line.ToLower().Contains("bAllowAWCAbilities".ToLower()))
                    {
                        readingClass.allowAwcAbilities = (getIntValue(line) == 1 || getNextStringValue(line, 0, classDataDelimitters) == "true");
                    }

                    if (line.ToLower().Contains("ExcludedAbilities".ToLower()))
                    {
                        string abilityName = getNextStringValue(line, 0, classDataDelimitters);
                        Ability ability = ProjectState.getAbility(abilityName);
                        if (ability != null)
                        {
                            readingClass.awcExcludeAbilities.Add(ability);
                        }
                    }

                    if (line.ToLower().Contains("bCanHaveBonds".ToLower()))
                    {
                        readingClass.metadata.allowBonds = (getIntValue(line) == 1 || getNextStringValue(line, 0, classDataDelimitters) == "true");
                    }

                    if (line.ToLower().Contains("UnfavoredClasses".ToLower()))
                    {
                        readingClass.metadata.unfavoredClasses.Add(getNextStringValue(line, 0, classDataDelimitters));
                    }

                    if (line.ToLower().Contains("BaseAbilityPointsPerPromotion".ToLower()))
                    {
                        readingClass.baseAbilityPointsPerPromotion = getIntValue(line);
                    }

                    // Ranks

                    if (line.ToLower().Contains("SoldierRanks".ToLower()))
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
                            string abilityName = getNextStringValue(line, tagStartIndex, classDataDelimitters);
                            WeaponSlot slot = WeaponSlot.None;

                            tagStartIndex = findStartingIndexOfNthOccurrence(line, "ApplyToWeaponSlot", counter, 0);
                            if (tagStartIndex > -1)
                            {
                                string slotString = getNextStringValue(line, tagStartIndex, classDataDelimitters);
                                slot = Enums.GetValueFromDescription<WeaponSlot>(slotString);
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
                                
                                string statTypeString = getNextStringValue(line, tagStartIndex, classDataDelimitters);
                                if (statTypeString != "eStat_CombatSims") // TODO handle this
                                {
                                    statType = Enums.GetValueFromDescription<Stat>(statTypeString);
                                }
                                
                                tagStartIndex = findStartingIndexOfNthOccurrence(line, "StatAmount", counter, 0);
                                string statAmountString = getNextStringValue(line, tagStartIndex, classDataDelimitters);

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

                    if (line.ToLower().Contains(" X2AbilityTemplate]".ToLower()))
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

                    if (line.ToLower().Contains(" X2SoldierClassTemplate]".ToLower()))
                    {
                        foundClass = new SoldierClass();
                        foundClass.metadata.internalName = line.Substring(1, line.IndexOf(' ') - 1);
                    }
                }
                else if(foundAbility != null)
                {
                    string value = getNextStringValue(line, 0, intDelimitters);

                    if (line.ToLower().Contains("LocFriendlyName".ToLower()))
                    {
                        foundAbility.displayName = value;
                    }
                    else if (line.ToLower().Contains("LocLongDescription".ToLower()))
                    {
                        foundAbility.description = value;
                    }
                }
                else if (foundClass != null)
                {
                    string value = getNextStringValue(line, 0, intDelimitters);
                    
                    if (line.ToLower().Contains("DisplayName".ToLower()))
                    {
                        foundClass.metadata.displayName = value;
                    }
                    else if (line.ToLower().Contains("ClassSummary".ToLower()))
                    {
                        foundClass.metadata.description = value;
                    }
                    else if (line.ToLower().Contains("LeftAbilityTreeTitle".ToLower()) || line.ToLower().Contains("AbilityTreeTitles[0]".ToLower()))
                    {
                        foundClass.leftTreeName = value;
                    }
                    else if (line.ToLower().Contains("AbilityTreeTitles[1]".ToLower()))
                    {
                        foundClass.rightTreeName = value;
                    }
                    else if (line.ToLower().Contains("RightAbilityTreeTitle".ToLower()) || line.ToLower().Contains("AbilityTreeTitles[2]".ToLower()))
                    {
                        foundClass.rightTreeName = value;
                    }
                    else if (line.ToLower().Contains("RandomNickNames[".ToLower()))
                    {
                        foundClass.nicknames.Add(new ClassNickname(value, NicknameGender.Unisex));
                    }
                    else if (line.ToLower().Contains("RandomNickNames_Male".ToLower()))
                    {
                        foundClass.nicknames.Add(new ClassNickname(value, NicknameGender.Male));
                    }
                    else if (line.ToLower().Contains("RandomNickNames_Female".ToLower()))
                    {
                        foundClass.nicknames.Add(new ClassNickname(value, NicknameGender.Female));
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

            // update classes based on data gathered
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
                    inLoadoutSection = line.ToLower().Contains("[XComGame.X2ItemTemplateManager]".ToLower());
                }
                else if (inLoadoutSection)
                {
                    if (line.ToLower().Contains("LoadoutName".ToLower()))
                    {
                        string loadoutName = getNextStringValue(line, line.IndexOf("LoadoutName"), classDataDelimitters);
                        SoldierClass soldierClass = soldierClasses.Where(x => x.equipment.squaddieLoadout == loadoutName).FirstOrDefault();
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
            string itemString = string.Format("Items[{0}]".ToLower(), itemsIndex.ToString());

            if (line.ToLower().Contains(itemString))
            {
                int startIndex = findStartingIndexOfNthOccurrence(line, itemString, 1, 0);

                // find the second equals after this for the item name
                startIndex = findStartingIndexOfNthOccurrence(line, "=", 2, startIndex);

                value = getNextStringValue(line, startIndex, classDataDelimitters);
            }

            return value;
        }

        private string getNextStringValue(string line, int startIndex, List<char> delimitters)
        {
            string value = string.Empty;

            int valueStartIndex = -1;
            int valueEndIndex = -1;

            int assignIndex = findStartingIndexOfNthOccurrence(line, "=", 1, startIndex);
            if (assignIndex != -1)
            {
                valueStartIndex = assignIndex + 1;
                while (valueStartIndex < line.Length &&
                        (delimitters.Contains(line[valueStartIndex])))
                {
                    valueStartIndex++;
                }

                valueStartIndex = Math.Min(valueStartIndex, line.Length);

                if (valueStartIndex != -1)
                {
                    valueEndIndex = valueStartIndex + 1;
                    while (valueEndIndex < line.Length &&
                          !(delimitters.Contains(line[valueEndIndex])))
                    {
                        valueEndIndex++;
                    }

                    valueEndIndex = Math.Min(valueEndIndex, line.Length);
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
                if (check.ToLower() == substring.ToLower())
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

        private int getIntValue(string line)
        {
            string stringValue = getNextStringValue(line, 0, classDataDelimitters);
            int.TryParse(stringValue, out int intValue);

            return intValue;
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
    }
}
