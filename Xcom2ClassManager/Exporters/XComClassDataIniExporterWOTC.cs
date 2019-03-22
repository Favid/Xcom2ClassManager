using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class XComClassDataIniExporterWOTC : XComClassDataIniExporter
    {
        public XComClassDataIniExporterWOTC(string folder, List<SoldierClass> soldiers) : base(folder, soldiers)
        {

        }

        protected override void writeClassGeneralData(SoldierClass soldier)
        {
            base.writeClassGeneralData(soldier);

            lines.Add("bCanHaveBonds=" + soldier.metadata.allowBonds);

            foreach (string unfavoredClass in soldier.metadata.unfavoredClasses)
            {
                lines.Add(String.Format("+UnfavoredClasses=\"{0}\"", unfavoredClass));
            }

            lines.Add("BaseAbilityPointsPerPromotion=" + soldier.baseAbilityPointsPerPromotion);
        }

        protected override string getClassAbilityData(SoldierClass soldier, SoldierRank rank)
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

                    thisAbility = string.Format("(AbilityType={0})", thisAbility);

                    abilityTree += thisAbility;
                }
            }

            abilityTree = "AbilitySlots=(" + abilityTree + "),";

            return abilityTree;
        }
    }
}
