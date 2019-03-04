using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xcom2ClassManager.DataObjects;
using Xcom2ClassManager.FileManagers;

namespace Xcom2ClassManager.Forms
{
    public partial class ImportClassesForm : Form
    {
        public ImportClassesForm()
        {
            InitializeComponent();
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tFileInt.Text) || string.IsNullOrEmpty(tFileClass.Text) || string.IsNullOrEmpty(tFileGame.Text))
            {
                return;
            }

            SoldierClassImporter importer = new SoldierClassImporter();
            List<SoldierClass> newClasses = importer.importSoldierClasses(tFileInt.Text, tFileClass.Text, tFileGame.Text);
            foreach (SoldierClass newClass in newClasses)
            {
                chListClasses.Items.Add(newClass);
            }
        }

        private void bBrowseClass_Click(object sender, EventArgs e)
        {
            ValidationResult validFolder = new ValidationResult();
            validFolder.valid = false;

            OpenFileDialog dialog;

            do
            {
                dialog = new OpenFileDialog();
                DialogResult dialogResult = dialog.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    validFolder = validateClassFile(dialog.FileName);

                    if (!validFolder.valid)
                    {
                        MessageBox.Show(validFolder.message);
                    }
                }
                else
                {
                    return;
                }
            } while (!validFolder.valid);

            tFileClass.Text = dialog.FileName;
        }

        private ValidationResult validateClassFile(string fileName)
        {
            ValidationResult validFile = new ValidationResult();
            validFile.valid = true;
            validFile.message = "";

            if (!fileName.EndsWith("XComClassData.ini", StringComparison.OrdinalIgnoreCase))
            {
                validFile.valid = false;
                validFile.message += "Must select XComClassData.ini file.";
                validFile.message += "\n";
            }

            if (!validFile.valid)
            {
                validFile.message += "Please choose a new file.";
            }

            return validFile;
        }

        private void bBrowseGame_Click(object sender, EventArgs e)
        {
            ValidationResult validFolder = new ValidationResult();
            validFolder.valid = false;

            OpenFileDialog dialog;

            do
            {
                dialog = new OpenFileDialog();
                DialogResult dialogResult = dialog.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    validFolder = validateGameFile(dialog.FileName);

                    if (!validFolder.valid)
                    {
                        MessageBox.Show(validFolder.message);
                    }
                }
                else
                {
                    return;
                }
            } while (!validFolder.valid);

            tFileGame.Text = dialog.FileName;
        }

        private ValidationResult validateGameFile(string fileName)
        {
            ValidationResult validFile = new ValidationResult();
            validFile.valid = true;
            validFile.message = "";

            if (!fileName.EndsWith("XComGameData.ini", StringComparison.OrdinalIgnoreCase))
            {
                validFile.valid = false;
                validFile.message += "Must select XComGameData.ini file.";
                validFile.message += "\n";
            }

            if (!validFile.valid)
            {
                validFile.message += "Please choose a new file.";
            }

            return validFile;
        }

        private void bBrowseInt_Click(object sender, EventArgs e)
        {
            ValidationResult validFolder = new ValidationResult();
            validFolder.valid = false;

            OpenFileDialog dialog;

            do
            {
                dialog = new OpenFileDialog();
                DialogResult dialogResult = dialog.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    validFolder = validateIntFile(dialog.FileName);

                    if (!validFolder.valid)
                    {
                        MessageBox.Show(validFolder.message);
                    }
                }
                else
                {
                    return;
                }
            } while (!validFolder.valid);

            tFileInt.Text = dialog.FileName;
        }

        private ValidationResult validateIntFile(string fileName)
        {
            ValidationResult validFile = new ValidationResult();
            validFile.valid = true;
            validFile.message = "";

            if (!fileName.EndsWith("XComGame.INT", StringComparison.OrdinalIgnoreCase))
            {
                validFile.valid = false;
                validFile.message += "Must select XComGame.INT file.";
                validFile.message += "\n";
            }

            if (!validFile.valid)
            {
                validFile.message += "Please choose a new file.";
            }

            return validFile;
        }

        private void chListClasses_SelectedValueChanged(object sender, EventArgs e)
        {
            CheckedListBox listbox = (CheckedListBox)sender;
            SoldierClass soldierClass = (SoldierClass)listbox.SelectedItem;

            tInternalName.Text = soldierClass.metadata.internalName;
            tDisplayName.Text = soldierClass.metadata.displayName;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            List<SoldierClass> selectedClasses = chListClasses.CheckedItems.OfType<SoldierClass>().ToList();

            foreach (SoldierClass soldierClass in selectedClasses)
            {
                ProjectState.addSoldierClass(soldierClass);
            }

            Close();
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            SoldierClass soldierClass = (SoldierClass)chListClasses.SelectedItem;
            
            soldierClass.metadata.internalName = tInternalName.Text;
            soldierClass.metadata.displayName = tDisplayName.Text;
        }
    }
}
