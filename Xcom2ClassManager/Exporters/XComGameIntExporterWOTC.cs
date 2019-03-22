using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class XComGameIntExporterWOTC : XComGameIntExporter
    {

        public XComGameIntExporterWOTC(string folder, List<SoldierClass> soldiers) : base(folder, soldiers)
        {

        }

        protected override void writeClassInt(SoldierClass soldier)
        {
            lines.Add("[" + soldier.metadata.internalName + " X2SoldierClassTemplate]");
            lines.Add("DisplayName=" + Utils.encaseStringInQuotes(soldier.metadata.displayName));
            lines.Add("ClassSummary=" + Utils.encaseStringInQuotes(soldier.metadata.description));
            lines.Add("LeftAbilityTreeTitle=" + Utils.encaseStringInQuotes(soldier.leftTreeName));
            lines.Add("RightAbilityTreeTitle=" + Utils.encaseStringInQuotes(soldier.rightTreeName));
            lines.Add("+AbilityTreeTitles[0]=" + Utils.encaseStringInQuotes(soldier.leftTreeName));
            lines.Add("+AbilityTreeTitles[1]=" + Utils.encaseStringInQuotes(soldier.middleTreeName));
            lines.Add("+AbilityTreeTitles[2]=" + Utils.encaseStringInQuotes(soldier.rightTreeName));
            lines.Add("");

            List<string> unisexNicknames = soldier.nicknames.Where(x => x.gender == NicknameGender.Unisex).Select(x => x.nickname).ToList();
            for (int i = 0; i < unisexNicknames.Count; i++)
            {
                lines.Add(Enums.getDescription(NicknameGender.Unisex) + Utils.getIndexString(i) + "=" + Utils.encaseStringInQuotes(unisexNicknames[i]));
            }

            List<string> maleNicknames = soldier.nicknames.Where(x => x.gender == NicknameGender.Male).Select(x => x.nickname).ToList();
            for (int i = 0; i < maleNicknames.Count; i++)
            {
                lines.Add(Enums.getDescription(NicknameGender.Male) + Utils.getIndexString(i) + "=" + Utils.encaseStringInQuotes(maleNicknames[i]));
            }

            List<string> femaleNicknames = soldier.nicknames.Where(x => x.gender == NicknameGender.Female).Select(x => x.nickname).ToList();
            for (int i = 0; i < femaleNicknames.Count; i++)
            {
                lines.Add(Enums.getDescription(NicknameGender.Female) + Utils.getIndexString(i) + "=" + Utils.encaseStringInQuotes(femaleNicknames[i]));
            }

            lines.Add("");
        }



    }
}
