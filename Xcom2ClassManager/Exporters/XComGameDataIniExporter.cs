using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.Exporters
{
    public class XComGameDataIniExporter
    {
        private const string FILENAME = "XComGameData.ini";

        private string folder { get; set; }
        private List<SoldierClass> soldiers { get; set; }
        private List<string> lines { get; set; }

        public XComGameDataIniExporter(string folder, List<SoldierClass> soldiers)
        {
            this.folder = folder;
            this.soldiers = soldiers;
            this.lines = new List<string>();
        }

        public void export(bool includeDebugStartingClasses)
        {
            lines.Add("[XComGame.X2ItemTemplateManager]");

            foreach (SoldierClass soldier in soldiers)
            {
                writeLoadout(soldier);
            }

            lines.Add("");

            if(includeDebugStartingClasses)
            {
                lines.Add("[XComGame.XGStrategy]");

                foreach(SoldierClass soldier in soldiers)
                {
                    writeDebugStartingClass(soldier);
                }
            }

            string filepath = Path.Combine(@folder, FILENAME);

            System.IO.File.WriteAllLines(@filepath, lines);
        }

        private void writeLoadout(SoldierClass soldier)
        {
            lines.Add(String.Format("+Loadouts=(LoadoutName={0}, {1})", Utils.encaseStringInQuotes(soldier.equipment.squaddieLoadout), getItemsString(soldier)));
        }

        private string getItemsString(SoldierClass soldier)
        {
            string itemsString = "";

            for(int i = 0; i < soldier.loadoutItems.Count; i++)
            {
                if (!string.IsNullOrEmpty(itemsString))
                {
                    itemsString += ", ";
                }

                itemsString += String.Format("Items[{0}]=(Item={1})", i, Utils.encaseStringInQuotes(soldier.loadoutItems[i]));
            }

            return itemsString;
        }

        private void writeDebugStartingClass(SoldierClass soldier)
        {
            lines.Add(String.Format("+DEBUG_StartingSoldierClasses={0}", soldier.metadata.internalName));
        }


    }
}
