using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xcom2ClassManager.DataObjects;

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
                    if(!requiredMods.Contains(classRequiredMod))
                    {
                        requiredMods.Add(classRequiredMod);
                    }
                }
            }

            requiredMods.Sort();

            lRequiredMods.Items.Clear();
            lRequiredMods.Items.AddRange(requiredMods.ToArray());
        }
    }
}
