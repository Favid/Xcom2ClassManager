using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class XComGameIntExporter
    {
        private const string FILENAME = "XComGame.INT";

        private string folder { get; set; }
        private List<SoldierClass> soldiers { get; set; }
        private List<string> lines { get; set; }

        public XComGameIntExporter(string folder, List<SoldierClass> soldiers)
        {
            this.folder = folder;
            this.soldiers = soldiers;
            this.lines = new List<string>();
        }

        public void export()
        {
            foreach(SoldierClass soldier in soldiers)
            {
                writeClassInt(soldier);
            }

            string filepath = Path.Combine(@folder, FILENAME);

            System.IO.File.WriteAllLines(@filepath, lines);
        }

        private void writeClassInt(SoldierClass soldier)
        {
            lines.Add("[" + soldier.metadata.internalName + " X2SoldierClassTemplate]");
            lines.Add("DisplayName=" + Utils.encaseStringInQuotes(soldier.metadata.displayName));
            lines.Add("ClassSummary=" + Utils.encaseStringInQuotes(soldier.metadata.description));
            lines.Add("LeftAbilityTreeTitle=" + Utils.encaseStringInQuotes(soldier.leftTreeName));
            lines.Add("RightAbilityTreeTitle=" + Utils.encaseStringInQuotes(soldier.rightTreeName));
            lines.Add("");

            List<string> unisexNicknames = soldier.nicknames.Where(x => x.gender == NicknameGender.Unisex).Select(x => x.nickname).ToList();
            for(int i = 0; i < unisexNicknames.Count; i++)
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
