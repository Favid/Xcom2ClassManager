using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class TLPExporter
    {
        private const string TQL_FILENAME = "XComTQL.ini";
        private const string CRO_FILENAME = "XComNoBaseGameClasses.ini";
        private const string LADDER_FILENAME = "XComLadder.ini";

        private string folder { get; set; }
        private List<SoldierClass> soldiers { get; set; }

        public TLPExporter(string folder, List<SoldierClass> soldiers)
        {
            this.folder = folder;
            this.soldiers = soldiers;
        }

        public void export()
        {
            exportXComTQLIni();
            exportXComNoBaseGameClassesIni();
            //exportXComLadderIni();
        }

        private void exportXComTQLIni()
        {
            List<string> lines = new List<string>();

            lines.Add("[XComGame.UITacticalQuickLaunch_MapData]");
            lines.Add("");

            // Iterate through classes
            foreach (SoldierClass soldier in soldiers)
            {
                // Iterate through ability slots
                for (int i = 1; i < 4; i++)
                {
                    string line = "";
                    foreach (SoldierRank rank in Enum.GetValues(typeof(SoldierRank)))
                    {
                        if (rank > SoldierRank.Rookie)
                        {
                            line = getLine(soldier, i, rank);
                            lines.Add(line);
                        }
                    }
                    lines.Add("");
                }
            }

            string filepath = Path.Combine(@folder, TQL_FILENAME);
            System.IO.File.WriteAllLines(@filepath, lines);
        }

        private string getLine(SoldierClass soldier, int abilitySlot, SoldierRank rank)
        {
            string filling =
                buildSoldierId(soldier, abilitySlot, rank, true) + ",    "
                + buildForceLevels(rank) + ",    "
                + "CharacterTemplate=\"Soldier\",    "
                + "SoldierClassTemplate=\"" + soldier.metadata.internalName + "\",    "
                + "SoldierRank=" + (int)rank + ",    "
                + buildPrimaryWeaponTemplate(soldier, rank) + ",    "
                + buildSecondaryWeaponTemplate(soldier, rank) + ",    "
                + "HeavyWeaponTemplate=\"\",    "
                + buildArmorTemplate(soldier, rank) + ",    "
                + buildUtilityItemTemplate(soldier, abilitySlot, rank, 1) + ",    "
                + buildUtilityItemTemplate(soldier, abilitySlot, rank, 2) + ",    "
                + buildGrenadeItemTemplate(soldier, abilitySlot, rank) + ",    "
                + buildAbilities(soldier, abilitySlot, rank)
                ;

            string line = string.Format("+Soldiers=({0})", filling);

            return line;
        }

        private string buildSoldierId(SoldierClass soldier, int abilitySlot, SoldierRank rank, bool includeAssignment)
        {
            string soldierId = soldier.metadata.internalName;
            switch (abilitySlot)
            {
                case 1:
                    soldierId += soldier.leftTreeName;
                    break;
                case 2:
                    soldierId += soldier.middleTreeName;
                    break;
                case 3:
                    soldierId += soldier.rightTreeName;
                    break;
            }

            soldierId += (int)rank;
            if (includeAssignment)
            {
                soldierId = string.Format("SoldierID=\"{0}\"", soldierId);
            }

            return soldierId;
        }

        private string buildForceLevels(SoldierRank rank)
        {
            string forceLevel = "";

            switch (rank)
            {
                case SoldierRank.Squaddie:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "1", "3");
                    break;
                case SoldierRank.Corporal:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "4", "5");
                    break;
                case SoldierRank.Sergeant:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "6", "8");
                    break;
                case SoldierRank.Lieutenant:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "9", "11");
                    break;
                case SoldierRank.Captain:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "12", "14");
                    break;
                case SoldierRank.Major:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "15", "17");
                    break;
                case SoldierRank.Colonel:
                case SoldierRank.Brigadier:
                    forceLevel = string.Format("MinForceLevel={0}, MaxForceLevel={1}", "18", "20");
                    break;
            }

            return forceLevel;
        }

        private string buildPrimaryWeaponTemplate(SoldierClass soldier, SoldierRank rank)
        {
            string primaryWeaponTemplate = buildWeaponTemplate(soldier, rank, WeaponSlot.Primary);
            primaryWeaponTemplate = string.Format("PrimaryWeaponTemplate=\"{0}\"", primaryWeaponTemplate);
            return primaryWeaponTemplate;
        }

        private string buildSecondaryWeaponTemplate(SoldierClass soldier, SoldierRank rank)
        {
            string secondaryWeaponTemplate = buildWeaponTemplate(soldier, rank, WeaponSlot.Secondary);
            secondaryWeaponTemplate = string.Format("SecondaryWeaponTemplate=\"{0}\"", secondaryWeaponTemplate);
            return secondaryWeaponTemplate;
        }

        private string buildWeaponTemplate(SoldierClass soldier, SoldierRank rank, WeaponSlot weaponSlot)
        {
            // assumes squaddie loadout weapoon ends with _CV
            // assumes primary is index 0 and secondary is index 1

            int index = 0;
            if (weaponSlot == WeaponSlot.Secondary)
            {
                index = 1;
            }

            string weaponTemplate = soldier.loadoutItems[index];
            string trimmed = weaponTemplate.Substring(0, weaponTemplate.Length - 2);

            switch (rank)
            {
                case SoldierRank.Squaddie:
                case SoldierRank.Corporal:
                    trimmed += "CV";
                    break;
                case SoldierRank.Sergeant:
                case SoldierRank.Lieutenant:
                    trimmed += "MG";
                    break;
                case SoldierRank.Captain:
                case SoldierRank.Major:
                case SoldierRank.Colonel:
                case SoldierRank.Brigadier:
                    trimmed += "BM";
                    break;
            }

            return trimmed;
        }

        private string buildArmorTemplate(SoldierClass soldier, SoldierRank rank)
        {
            string armorTemplate = string.Empty;

            switch (rank)
            {
                case SoldierRank.Squaddie:
                case SoldierRank.Corporal:
                case SoldierRank.Sergeant:
                    armorTemplate = "KevlarArmor";
                    break;
                case SoldierRank.Lieutenant:
                case SoldierRank.Captain:
                    armorTemplate = "MediumPlatedArmor";
                    break;
                case SoldierRank.Major:
                case SoldierRank.Colonel:
                case SoldierRank.Brigadier:
                    armorTemplate = "MediumPoweredArmor";
                    break;
            }

            armorTemplate = string.Format("ArmorTemplate=\"{0}\"", armorTemplate);
            return armorTemplate;
        }

        private string buildUtilityItemTemplate(SoldierClass soldier, int abilitySlot, SoldierRank rank, int utilitySlot)
        {
            string templateName = "FragGrenade";
            if (rank >= SoldierRank.Lieutenant)
            {
                templateName = "AlienGrenade";
            }

            // going to get hacky and put in some hardcoded values for now...

            templateName = string.Format("UtilityItem{0}Template=\"{1}\"", utilitySlot, templateName);

            return templateName;
        }

        private string buildGrenadeItemTemplate(SoldierClass soldier, int abilitySlot, SoldierRank rank)
        {
            string templateName = string.Empty;

            // going to get hacky and put in some hardcoded values for now...
            if (soldier.equipment.weapons.Any(x => x.weaponName == "grenade_launcher"))
            {
                templateName = "FragGrenade";
                if (rank >= SoldierRank.Lieutenant)
                {
                    templateName = "AlienGrenade";
                }
            }
            
            templateName = string.Format("GrenadeSlotTemplate=\"{0}\"", templateName);

            return templateName;
        }

        private string buildAbilities(SoldierClass soldier, int abilitySlot, SoldierRank rank)
        {
            string earnedAbilities = string.Empty;

            List<string> abilities = new List<string>();

            // always add squaddie abilities
            abilities = soldier.soldierAbilities.Where(x => x != null && x.rank == SoldierRank.Squaddie && x.internalName != string.Empty).Select(x => x.internalName).ToList();

            // start at rank after squaddie
            for (int i = 2; i <= (int)rank; i++)
            {
                SoldierRank thisRank = (SoldierRank)i;
                SoldierClassAbility thisAbility = soldier.getSoldierAbility(thisRank, abilitySlot);
                if (thisAbility != null && thisAbility.internalName != string.Empty)
                {
                    abilities.Add(thisAbility.internalName);
                }
            }

            // now build the string
            int index = 0;
            foreach (string ability in abilities)
            {
                if (earnedAbilities != string.Empty)
                {
                    earnedAbilities += ",    ";
                }

                earnedAbilities += string.Format("EarnedClassAbilities[{0}]=\"{1}\"", index, ability);
                index++;
            }

            return earnedAbilities;
        }

        private void exportXComNoBaseGameClassesIni()
        {
            List<string> lines = new List<string>();

            lines.Add("[WOTC_RA_NoBaseGameClasses.X2DownloadableContentInfo_WOTC_RA_NoBaseGameClasses]");
            lines.Add("");

            // Iterate through classes
            foreach (SoldierClass soldier in soldiers)
            {
                // Iterate through ability slots
                for (int i = 1; i < 4; i++)
                {
                    lines.Add("+RandomProgressions = (	Members[0] = \"" + buildSoldierId(soldier, i, SoldierRank.Squaddie, false) + "\", \\\\");
                    lines.Add("						Members[1] = \"" + buildSoldierId(soldier, i, SoldierRank.Corporal, false) + "\", \\\\");
                    lines.Add("						Members[2] = \"" + buildSoldierId(soldier, i, SoldierRank.Sergeant, false) + "\", \\\\");
                    lines.Add("						Members[3] = \"" + buildSoldierId(soldier, i, SoldierRank.Lieutenant, false) + "\", \\\\");
                    lines.Add("						Members[4] = \"" + buildSoldierId(soldier, i, SoldierRank.Captain, false) + "\", \\\\");
                    lines.Add("						Members[5] = \"" + buildSoldierId(soldier, i, SoldierRank.Major, false) + "\", \\\\");
                    lines.Add("						Members[6] = \"" + buildSoldierId(soldier, i, SoldierRank.Colonel, false) + "\", \\\\");
                    lines.Add("						Members[7] = \"" + buildSoldierId(soldier, i, SoldierRank.Brigadier, false) + "\", \\\\");
                    lines.Add("						Members[8] = \"" + buildSoldierId(soldier, i, SoldierRank.Brigadier, false) + "\" )");
                    lines.Add("");
                }
            }
            
            string filepath = Path.Combine(@folder, CRO_FILENAME);
            System.IO.File.WriteAllLines(@filepath, lines);
        }

        private void exportXComLadderIni()
        {
            List<string> lines = new List<string>();



            string filepath = Path.Combine(@folder, LADDER_FILENAME);
            System.IO.File.WriteAllLines(@filepath, lines);
        }



    }
}
