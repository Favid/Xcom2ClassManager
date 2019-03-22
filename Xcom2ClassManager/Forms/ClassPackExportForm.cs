using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xcom2ClassManager.DataObjects;
using Xcom2ClassManager.Exporters;

namespace Xcom2ClassManager.Forms
{
    public partial class ClassPackExportForm : Form
    {
        private ClassPack classPack;

        public ClassPackExportForm(ClassPack classPack)
        {
            InitializeComponent();

            this.classPack = classPack;
        }

        private void ClassPackExportForm_Load(object sender, EventArgs e)
        {
            foreach(SoldierClass soldierClass in classPack.soldierClasses)
            {
                chlClasses.Items.Add(soldierClass, true);
            }
        }

        private void chlClasses_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if(chlClasses.CheckedItems.Count <=1 && e.NewValue == CheckState.Unchecked)
            {
                lRequiredMods.Items.Clear();
                return;
            }

            List<SoldierClass> checkedItems = new List<SoldierClass>();
            for (int i = 0; i < chlClasses.CheckedItems.Count; i++)
            {
                if(e.NewValue == CheckState.Unchecked && i == e.Index)
                {
                    continue;
                }

                checkedItems.Add((SoldierClass)chlClasses.CheckedItems[i]);
            }
            
            if (e.NewValue == CheckState.Checked)
            {
                checkedItems.Add((SoldierClass)chlClasses.Items[e.Index]);
            }

            List<string> requiredMods = new List<string>();
            foreach (SoldierClass soldierClass in checkedItems)
            {
                List<string> classRequiredMods = soldierClass.getRequiredMods();
                foreach(string classRequiredMod in classRequiredMods)
                {
                    if(!requiredMods.Contains(classRequiredMod) && classRequiredMod != "None")
                    {
                        requiredMods.Add(classRequiredMod);
                    }
                }
            }

            requiredMods.Sort();

            lRequiredMods.Items.Clear();
            lRequiredMods.Items.AddRange(requiredMods.ToArray());
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tDestination.Text = dialog.SelectedPath;
            }
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            string destination = tDestination.Text;
            List<SoldierClass> soldiersToExport = chlClasses.CheckedItems.Cast<SoldierClass>().ToList();

            if (chXcomClassDataIni.Checked)
            {
                XComClassDataIniExporterWOTC exporter = new XComClassDataIniExporterWOTC(destination, soldiersToExport);
                exporter.export();
            }

            if(chXcomGameInt.Checked)
            {
                XComGameIntExporterWOTC exporter = new XComGameIntExporterWOTC(destination, soldiersToExport);
                exporter.export();
            }

            if(chXcomGameDataIni.Checked)
            {
                XComGameDataIniExporter exporter = new XComGameDataIniExporter(destination, soldiersToExport);
                exporter.export(chDebugClasses.Checked);
            }

            Process.Start(destination);
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
