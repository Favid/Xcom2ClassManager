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
    public partial class ImportAbilitiesForm : Form
    {
        private List<Ability> allAbilities;
        private OverviewForm overviewForm;

        public ImportAbilitiesForm(OverviewForm overviewForm, List<Ability> allAbilities)
        {
            InitializeComponent();

            this.overviewForm = overviewForm;
            this.allAbilities = allAbilities;

            List<WeaponSlot> weaponSlots = new List<WeaponSlot>();
            weaponSlots.Add(WeaponSlot.None);
            weaponSlots.Add(WeaponSlot.Unknown);
            weaponSlots.Add(WeaponSlot.Primary);
            weaponSlots.Add(WeaponSlot.Secondary);
            weaponSlots.Add(WeaponSlot.Heavy);

            cWeaponSlot.DataSource = weaponSlots;

            cWeaponSlot.SelectedIndex = 0;
        }

        private void bImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tFileInt.Text) || string.IsNullOrEmpty(tModName.Text))
            {
                return;
            }
            
            SoldierClassImporter importer = new SoldierClassImporter();

            List<Ability> newAbilities;

            if (string.IsNullOrEmpty(tFileClass.Text) || tFileClass.Text == "     (Optional)")
            {
                newAbilities = importer.importAbilities(tFileInt.Text, tModName.Text);
            }
            else
            {
                newAbilities = importer.importAbilities(tFileInt.Text, tFileClass.Text, tModName.Text);
            }
            
            newAbilities = newAbilities.OrderBy(x => x.internalName).ToList();
            foreach (Ability foundAbility in newAbilities)
            {
                chListAbilities.Items.Add(foundAbility);
            }
        }

        private Ability importAbility(StreamReader file, string startingLine)
        {
            Ability ability = new Ability();
            ability.internalName = startingLine.Substring(1, startingLine.IndexOf(' ') - 1);
            
            string nextLine = file.ReadLine();
            while (!string.IsNullOrEmpty(nextLine))
            {
                int startIndex = nextLine.IndexOf('"') + 1;
                int endIndex = nextLine.LastIndexOf('"');

                if (nextLine.Contains("LocFriendlyName="))
                {
                    ability.displayName = nextLine.Substring(startIndex, endIndex - startIndex);
                }
                else if (nextLine.Contains("LocLongDescription="))
                {
                    ability.description = nextLine.Substring(startIndex, endIndex - startIndex);
                }

                nextLine = file.ReadLine();
            }

            return ability;
        }

        private void bBrowseInt_Click(object sender, EventArgs e)
        {
            tFileInt.Text = browse("XComGame.INT");
            //updateControls();
        }
        private void bBrowseClass_Click(object sender, EventArgs e)
        {
            tFileClass.Text = browse("XComClassData.ini");
            //updateControls();
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

        private void chListClasses_SelectedValueChanged(object sender, EventArgs e)
        {
            CheckedListBox listbox = (CheckedListBox)sender;
            Ability selectedAbility = (Ability)listbox.SelectedItem;

            tInternalName.Text = selectedAbility.internalName;
            tDisplayName.Text = selectedAbility.displayName;
            tDescription.Text = selectedAbility.description;
            cWeaponSlot.SelectedItem = selectedAbility.weaponSlot;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            List<Ability> selectedAbilities = chListAbilities.CheckedItems.OfType<Ability>().ToList();
            
            AbilityManager abilityManager = new AbilityManager();
            abilityManager.addAbilities(selectedAbilities);

            overviewForm.reloadAbilities();

            Close();
        }

        private void tInternalName_Leave(object sender, EventArgs e)
        {
            Ability selectedAbility = (Ability)chListAbilities.SelectedItem;
            selectedAbility.internalName = tInternalName.Text;

            chListAbilities.Refresh();
        }

        private void tDisplayName_Leave(object sender, EventArgs e)
        {
            Ability selectedAbility = (Ability)chListAbilities.SelectedItem;
            selectedAbility.displayName = tDisplayName.Text;

            chListAbilities.Refresh();
        }

        private void tDescription_Leave(object sender, EventArgs e)
        {
            Ability selectedAbility = (Ability)chListAbilities.SelectedItem;
            selectedAbility.description = tDescription.Text;

            chListAbilities.Refresh();
        }

        private void cWeaponSlot_SelectedValueChanged(object sender, EventArgs e)
        {
            Ability selectedAbility = (Ability)chListAbilities.SelectedItem;

            if (selectedAbility != null)
            {
                selectedAbility.weaponSlot = (WeaponSlot)cWeaponSlot.SelectedItem;
                chListAbilities.Refresh();
            }
        }
    }
}
