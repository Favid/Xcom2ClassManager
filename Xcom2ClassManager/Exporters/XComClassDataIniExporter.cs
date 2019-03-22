using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class XComClassDataIniExporter
    {
        protected const string FILENAME = "XComClassData.ini";
        
        protected string folder { get; set; }
        protected List<SoldierClass> soldiers { get; set; }
        protected List<string> lines { get; set; }

        public XComClassDataIniExporter(string folder, List<SoldierClass> soldiers)
        {
            this.folder = folder;
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

            string filepath = Path.Combine(@folder, FILENAME);

            System.IO.File.WriteAllLines(@filepath, lines);
        }

        protected virtual void writeDefaultClasses()
        {
            lines.Add("[XComGame.X2SoldierClass_DefaultClasses]");
            foreach (SoldierClass soldier in soldiers)
            {
                lines.Add("+SoldierClasses=" + soldier.metadata.internalName);
            }

            lines.Add("");
        }

        protected virtual void writeClass(SoldierClass soldier)
        {
            writeClassGeneralData(soldier);
            writeClassRanksData(soldier);
        }

        protected virtual void writeClassGeneralData(SoldierClass soldier)
        {
            lines.Add("[" + soldier.metadata.internalName + " X2SoldierClassTemplate]");
            lines.Add("bMultiplayerOnly=0");
            lines.Add("ClassPoints=4");
            lines.Add("IconImage=" + Utils.encaseStringInQuotes(soldier.metadata.iconString));
            lines.Add("NumInForcedDeck=" + soldier.experience.numberInForcedDeck);
            lines.Add("NumInDeck=" + soldier.experience.numberInDeck);
            lines.Add("KillAssistsPerKill=" + soldier.experience.killAssistsPerKill);
            lines.Add("SquaddieLoadout=" + Utils.encaseStringInQuotes(soldier.equipment.squaddieLoadout));

            foreach (Ability ability in soldier.awcExcludeAbilities)
            {
                lines.Add(String.Format("+ExcludedAbilities=\"{0}\"", ability.internalName));
            }

            writeClassWeapons(soldier);

            foreach (string armor in soldier.equipment.allowedArmors)
            {
                lines.Add(String.Format("+AllowedArmors=\"{0}\"", armor));
            }

            lines.Add("bAllowAWCAbilities=" + (soldier.allowAwcAbilities ? "1" : "0"));
        }

        protected virtual void writeClassWeapons(SoldierClass soldier)
        {
            foreach(Weapon weapon in soldier.equipment.weapons)
            {
                lines.Add(String.Format("+AllowedWeapons=(SlotType={0}, WeaponType=\"{1}\")", Enums.getDescription(weapon.weaponSlot), weapon.weaponName));
            }
        }

        protected virtual void writeClassRanksData(SoldierClass soldier)
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

        protected virtual void writeClassRankData(SoldierClass soldier, SoldierRank rank)
        {
            string rankString = "+SoldierRanks=";

            rankString += getClassAbilityData(soldier, rank);
            rankString += getClassStatData(soldier, rank);

            lines.Add(";" + rank.ToString());
            lines.Add(rankString);
            lines.Add("");
        }

        protected virtual string getClassAbilityData(SoldierClass soldier, SoldierRank rank)
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
                    
                    thisAbility += "(AbilityName=" + Utils.encaseStringInQuotes(soldierAbility.internalName);

                    if (soldierAbility.weaponSlot != WeaponSlot.None)
                    {
                        thisAbility += ", ApplyToWeaponSlot=" + Enums.getDescription(soldierAbility.weaponSlot);
                    }

                    thisAbility += ")";

                    abilityTree += thisAbility;
                }
            }

            abilityTree = "( aAbilityTree=(" + abilityTree + "),";

            return abilityTree;
        }

        protected virtual string getClassStatData(SoldierClass soldier, SoldierRank rank)
        {
            List<SoldierClassStat> rankStats = soldier.stats.Where(x => x.rank == rank).OrderBy(x => x.stat).ToList();

            string fullStat = "";

            if (rank == SoldierRank.Squaddie)
            {
                fullStat = "(StatType=eStat_CombatSims,StatAmount=1)";
            }

            foreach (SoldierClassStat squaddieStat in rankStats)
            {
                if (squaddieStat.value > 0)
                {
                    string thisStat = "";
                    if (!string.IsNullOrEmpty(fullStat))
                    {
                        thisStat = ",";
                    }

                    thisStat += "(StatType=" + Enums.getDescription(squaddieStat.stat) + ",StatAmount=" + squaddieStat.value.ToString() + ")";
                    fullStat += thisStat;
                }
            }

            fullStat = "aStatProgression=(" + fullStat + "))";

            return fullStat;
        }
    }
}
