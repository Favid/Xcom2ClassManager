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

        public ImportAbilitiesForm(List<Ability> allAbilities)
        {
            InitializeComponent();

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
            if (string.IsNullOrEmpty(tFile.Text))
            {
                return;
            }

            List<Ability> foundAbilities = new List<Ability>();

            int counter = 0;
            string line;

            StreamReader file = new StreamReader(tFile.Text);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(" X2AbilityTemplate]"))
                {
                    Ability foundAbility = importAbility(file, line);

                    // TODO Kind of a hack - Ignoresabilities whose names already exist
                    // Will probably want an overwrite option in the future
                    if (foundAbilities.Where(x => x.internalName.Equals(foundAbility.internalName, StringComparison.OrdinalIgnoreCase)).Count() == 0
                        && allAbilities.Where(x => x.internalName.Equals(foundAbility.internalName, StringComparison.OrdinalIgnoreCase)).Count() == 0)
                    {
                        foundAbilities.Add(foundAbility);
                    }
                }
                counter++;
            }

            file.Close();

            foundAbilities = foundAbilities.OrderBy(x => x.internalName).ToList();
            foreach (Ability foundAbility in foundAbilities)
            {
                chListClasses.Items.Add(foundAbility);
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

        private void bBrowse_Click(object sender, EventArgs e)
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

            tFile.Text = dialog.FileName;
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
            List<Ability> selectedAbilities = chListClasses.CheckedItems.OfType<Ability>().ToList();
            foreach (Ability ability in selectedAbilities)
            {
                ability.requiredMod = tRequiredMod.Text;
            }
            AbilityManager reader = new AbilityManager();
            reader.addAbilities(selectedAbilities);
            ProjectState.reloadAbilities();
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            Ability selectedAbility = (Ability)chListClasses.SelectedItem;
            selectedAbility.internalName = tInternalName.Text;
            selectedAbility.displayName = tDisplayName.Text;
            selectedAbility.description = tDescription.Text;
            selectedAbility.weaponSlot = (WeaponSlot)cWeaponSlot.SelectedItem;


        }
    }
}
