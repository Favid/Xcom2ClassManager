using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xcom2ClassManager.DataObjects;
using Xcom2ClassManager.FileManagers;

namespace Xcom2ClassManager.Forms
{
    public partial class ImportClassesForm : Form
    {
        private bool imported;

        public ImportClassesForm()
        {
            InitializeComponent();

            imported = false;

            updateControls();
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
                chListClasses.Items.Add(newClass, true);
            }

            imported = true;

            updateControls();
        }

        private void bBrowseClass_Click(object sender, EventArgs e)
        {
            tFileClass.Text = browse("XComClassData.ini");
            updateControls();
        }

        private void bBrowseGame_Click(object sender, EventArgs e)
        {
            tFileGame.Text = browse("XComGameData.ini");
            updateControls();
        }

        private void bBrowseInt_Click(object sender, EventArgs e)
        {
            tFileInt.Text = browse("XComGame.INT");
            updateControls();
        }

        private string browse(string expectedFileName)
        {
            string result = string.Empty;

            ValidationResult validFolder = new ValidationResult();
            validFolder.valid = false;

            OpenFileDialog dialog;

            do
            {
                dialog = new OpenFileDialog();
                DialogResult dialogResult = dialog.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    validFolder = validateFile(dialog.FileName, expectedFileName);

                    if (!validFolder.valid)
                    {
                        MessageBox.Show(validFolder.message);
                    }
                }
                else
                {
                    return result;
                }
            } while (!validFolder.valid);

            result = dialog.FileName;

            return result;
        }

        private void chListClasses_SelectedValueChanged(object sender, EventArgs e)
        {
            CheckedListBox listbox = (CheckedListBox)sender;
            SoldierClass soldierClass = (SoldierClass)listbox.SelectedItem;

            tInternalName.Text = soldierClass.metadata.internalName;
            tDisplayName.Text = soldierClass.metadata.displayName;

            updateControls();
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

            chListClasses.Refresh();

            updateControls();
        }
        
        private ValidationResult validateFile(string fileName, string expectedFileName)
        {
            ValidationResult validFile = new ValidationResult();
            validFile.valid = true;
            validFile.message = "";

            if (!fileName.EndsWith(expectedFileName, StringComparison.OrdinalIgnoreCase))
            {
                validFile.valid = false;
                validFile.message += string.Format("Must select {0} file.", expectedFileName);
                validFile.message += "\n";
            }

            if (!validFile.valid)
            {
                validFile.message += "Please choose a new file.";
            }

            return validFile;
        }

        private void updateControls()
        {
            bool filesSelected = (tFileClass.Text != string.Empty &&
                                  tFileGame.Text != string.Empty &&
                                  tFileInt.Text != string.Empty);

            bImport.Enabled = !imported && filesSelected;
            bBrowseClass.Enabled = !imported;
            bBrowseGame.Enabled = !imported;
            bBrowseInt.Enabled = !imported;

            chListClasses.Enabled = imported;
            tInternalName.Enabled = chListClasses.SelectedIndex >= 0;
            tDisplayName.Enabled = chListClasses.SelectedIndex >= 0;
            bUpdate.Enabled = chListClasses.SelectedIndex >= 0;

            List<SoldierClass> selectedClasses = chListClasses.CheckedItems.OfType<SoldierClass>().ToList();

            bool allNamesUnique = true;
            foreach (SoldierClass soldierClass in selectedClasses)
            {
                if (ProjectState.soldierWithNameExists(soldierClass.metadata.internalName))
                {
                    allNamesUnique = false;
                }
            }

            bSave.Enabled = allNamesUnique && imported && chListClasses.CheckedItems.Count > 0;
        }

        private void chListClasses_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            updateControls();
        }
    }
}
