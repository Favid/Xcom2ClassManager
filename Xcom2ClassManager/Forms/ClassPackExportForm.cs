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
    }
}
