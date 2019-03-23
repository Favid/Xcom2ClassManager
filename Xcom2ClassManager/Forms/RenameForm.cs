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
            bool validName =
                !string.IsNullOrEmpty(tNewName.Text) &&
                    (originalInternalName.Equals(tNewName.Text) ||
                    !ProjectState
                        .getClassPack()
                        .soldierClasses
                        .Where(x => x.metadata.internalName.Equals(tNewName.Text))
                        .Any()
                     );

            if (validName)
            {
                newInternalName = tNewName.Text;
                Close();
            }
            else
            {
                MessageBox.Show("Class name must be unique");
            }
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

        private void tNewName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter key pressed
            if (e.KeyChar == (char)13)
            {
                bOk_Click(null, null);
            }
        }
    }
}
