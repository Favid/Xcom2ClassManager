using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xcom2ClassManager.Forms
{
    public partial class RenameForm : Form
    {
        private string originalInternalName;
        private string newInternalName;

        public RenameForm(string originalInternalName)
        {
            InitializeComponent();

            this.originalInternalName = originalInternalName;

            newInternalName = this.originalInternalName;
            tNewName.Text = newInternalName;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            // TODO validate name

            newInternalName = tNewName.Text;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            newInternalName = originalInternalName;
            Close();
        }

        public string getNewName()
        {
            return newInternalName;
        }

        public string getOldName()
        {
            return originalInternalName;
        }
    }
}
