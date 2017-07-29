using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.ExportForXcom
{
    public class ClassExportManager
    {
        private string filePath { get; set; }
        private List<SoldierClass> soldiers { get; set; }
        private List<string> lines { get; set; }

        public ClassExportManager()
        {
            this.filePath = "";
            this.soldiers = new List<SoldierClass>();
            this.lines = new List<string>();
        }

        public ClassExportManager(string filePath, List<SoldierClass> soldiers)
        {
            this.filePath = filePath;
            this.soldiers = soldiers;
            this.lines = new List<string>();
        }

        public void export()
        {
            // write Default Classes
            writeDefaultClasses();

            // write individual classes
            foreach (SoldierClass soldier in soldiers)
            {
                writeClass(soldier);
            }

            System.IO.File.WriteAllLines(@filePath, lines);
        }

        private void writeDefaultClasses()
        {
            lines.Add("[XComGame.X2SoldierClass_DefaultClasses]");
            foreach (SoldierClass soldier in soldiers)
            {
                lines.Add("+SoldierClasses=" + soldier.metadata.internalName);
            }

            lines.Add("");
        }

        private void writeClass(SoldierClass soldier)
        {
            writeClassGeneralData(soldier);
            writeClassRanksData(soldier);
        }

        private void writeClassGeneralData(SoldierClass soldier)
        {
            lines.Add("[" + soldier.metadata.internalName + " X2SoldierClassTemplate]");
            lines.Add("+bMultiplayerOnly=0");
            lines.Add("+ClassPoints=4");
            lines.Add("+IconImage=" + soldier.metadata.iconString);
            lines.Add("+NumInForcedDeck=" + soldier.experience.numberInForcedDeck);
            lines.Add("+NumInDeck=" + soldier.experience.numberInDeck);
            lines.Add("+KillAssistsPerKill=" + soldier.experience.killAssistsPerKill);
            lines.Add("+SquaddieLoadout=" + soldier.equipment.squaddieLoadout);
            lines.Add("+bAllowAWCAbilities=1");
            // TODO weapons
            //lines.Add(String.Format("+AllowedWeapons=(SlotType=eInvSlot_PrimaryWeapon, WeaponType=\"{0}\")", soldier.equipment.primaryWeapon));
            //lines.Add(String.Format("+AllowedWeapons=(SlotType=eInvSlot_SecondaryWeapon, WeaponType=\"{0}\")", soldier.equipment.secondaryWeapon));
            lines.Add(String.Format("+AllowedArmors=\"{0}\"", soldier.equipment.allowedArmors));
        }

        private void writeClassRanksData(SoldierClass soldier)
        {
            writeClassRankData(soldier, SoldierRank.Squaddie);
            writeClassRankData(soldier, SoldierRank.Corporal);
            writeClassRankData(soldier, SoldierRank.Sergeant);
            writeClassRankData(soldier, SoldierRank.Lieutenant);
            writeClassRankData(soldier, SoldierRank.Captain);
            writeClassRankData(soldier, SoldierRank.Major);
            writeClassRankData(soldier, SoldierRank.Colonel);
            writeClassRankData(soldier, SoldierRank.Brigadier);
        }

        private void writeClassRankData(SoldierClass soldier, SoldierRank rank)
        {
            string rankString = "+SoldierRanks=";

            rankString += getClassAbilityData(soldier, rank);
            rankString += getClassStatData(soldier, rank);

            lines.Add(";" + rank.ToString());
            lines.Add(rankString);
            lines.Add("");
        }

        private string getClassAbilityData(SoldierClass soldier, SoldierRank rank)
        {
            List<SoldierClassAbility> rankSoldierAbilities = soldier.soldierAbilities.Where(x => x.rank == rank).OrderBy(x => x.slot).ToList();

            string abilityTree = "";

            foreach (SoldierClassAbility soldierAbility in rankSoldierAbilities)
            {
                if (!string.IsNullOrEmpty(soldierAbility.internalName))
                {
                    string thisAbility = "";
                    if (!string.IsNullOrEmpty(abilityTree))
                    {
                        thisAbility = ",";
                    }

                    //thisAbility += "(AbilityName=" + Utils.encaseStringInQuotes(soldierAbility.internalName) + ", ApplyToWeaponSlot=" + getIniWeaponSlot(soldierAbility.weaponSlot) + ")";

                    thisAbility += "(AbilityName=" + Utils.encaseStringInQuotes(soldierAbility.internalName);

                    if (soldierAbility.weaponSlot != WeaponSlot.None)
                    {
                        thisAbility += ", ApplyToWeaponSlot=" + getIniWeaponSlot(soldierAbility.weaponSlot);
                    }

                    thisAbility += ")";

                    abilityTree += thisAbility;
                }
            }

            abilityTree = "( aAbilityTree=(" + abilityTree + "),";

            return abilityTree;
        }

        private string getClassStatData(SoldierClass soldier, SoldierRank rank)
        {
            List<SoldierClassStat> rankStats = soldier.stats.Where(x => x.rank == rank).OrderBy(x => x.stat).ToList();

            string fullStat = "";

            foreach (SoldierClassStat squaddieStat in rankStats)
            {
                if (squaddieStat.value > 0)
                {
                    string thisStat = "";
                    if (!string.IsNullOrEmpty(fullStat))
                    {
                        thisStat = ",";
                    }

                    thisStat += "(StatType=" + getIniStat(squaddieStat.stat) + ",StatAmount=" + squaddieStat.value.ToString() + ")";
                    fullStat += thisStat;
                }
            }

            fullStat = "aStatProgression=(" + fullStat + "))";

            return fullStat;
        }

        private string getIniWeaponSlot(WeaponSlot weaponSlot)
        {
            switch (weaponSlot)
            {
                case WeaponSlot.Primary:
                    return "eInvSlot_PrimaryWeapon";

                case WeaponSlot.Secondary:
                    return "eInvSlot_SecondaryWeapon";

                case WeaponSlot.Unknown:
                    return "eInvSlot_Unknown";

                case WeaponSlot.None:
                    return "";
            }

            return "";
        }

        private string getIniStat(Stat stat)
        {
            switch (stat)
            {
                case Stat.HP:
                    return "eStat_HP";

                case Stat.Aim:
                    return "eStat_Offense";

                case Stat.Strength:
                    return "eStat_Strength";

                case Stat.Hacking:
                    return "eStat_Hacking";

                case Stat.Psi:
                    return "eStat_PsiOffense";
            }

            return "";
        }

        public void exportInt()
        {
            foreach (SoldierClass soldier in soldiers)
            {
                writeClassInt(soldier);
            }

            System.IO.File.WriteAllLines(@filePath, lines);
        }

        private void writeClassInt(SoldierClass soldier)
        {
            lines.Add("[" + soldier.metadata.internalName + " X2SoldierClassTemplate]");
            lines.Add("DisplayName=" + Utils.encaseStringInQuotes(soldier.metadata.displayName));
            lines.Add("ClassSummary=" + Utils.encaseStringInQuotes(soldier.metadata.description));
            lines.Add("LeftAbilityTreeTitle=" + Utils.encaseStringInQuotes("Left"));
            lines.Add("RightAbilityTreeTitle=" + Utils.encaseStringInQuotes("Right"));
            lines.Add("");
        }
    }
}
