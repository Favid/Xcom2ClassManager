using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class XComLWOverhaulIniExporter
    {
        private const string FILENAME = "XComLW_Overhaul.ini";

        private string folder { get; set; }
        private List<SoldierClass> soldiers { get; set; }
        private List<string> lines { get; set; }

        public XComLWOverhaulIniExporter(string folder, List<SoldierClass> soldiers)
        {
            this.folder = folder;
            this.soldiers = soldiers;
            this.lines = new List<string>();
        }

        public void export()
        {
            lines.Add("[LW_Overhaul.XComGameState_LWListenerManager]");

            foreach (SoldierClass soldier in soldiers)
            {
                writeExperienceWeight(soldier);
            }

            string filepath = Path.Combine(@folder, FILENAME);

            System.IO.File.WriteAllLines(@filepath, lines);
        }

        private void writeExperienceWeight(SoldierClass soldier)
        {
            lines.Add(String.Format("+CLASS_MISSION_EXPERIENCE_WEIGHTS=(SoldierClass={0}, MissionExperienceWeight={1}f)", Utils.encaseStringInQuotes(soldier.metadata.internalName), soldier.experience.missionExperienceWeight.ToString()));
        }

    }
}
