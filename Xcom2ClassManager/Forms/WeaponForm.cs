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
    public partial class WeaponForm : Form
    {
        public Weapon oldWeapon { get; private set; }
        public Weapon newWeapon { get; private set; }

        public EditorState editorState { get; private set; }

        private void WeaponEditor_Load(object sender, EventArgs e)
        {

        }

        public WeaponForm(Weapon weapon, EditorState editorState)
        {
            InitializeComponent();

            if (weapon == null)
            {
                oldWeapon = new Weapon();
                newWeapon = new Weapon();
            }
            else
            {
                oldWeapon = new Weapon(weapon);
                newWeapon = new Weapon(weapon);
            }

            tWeaponName.Text = oldWeapon.weaponName;

            List<WeaponSlot> weaponSlots = new List<WeaponSlot>();
            weaponSlots.Add(WeaponSlot.None);
            weaponSlots.Add(WeaponSlot.Unknown);
            weaponSlots.Add(WeaponSlot.Primary);
            weaponSlots.Add(WeaponSlot.Secondary);
            weaponSlots.Add(WeaponSlot.Heavy);

            cWeaponSlot.DataSource = weaponSlots;

            cWeaponSlot.SelectedIndex = cWeaponSlot.Items.IndexOf(oldWeapon.weaponSlot);

            this.editorState = editorState;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            // TODO validate

            newWeapon.weaponName = tWeaponName.Text;
            newWeapon.weaponSlot = (WeaponSlot)cWeaponSlot.SelectedItem;

            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            newWeapon = oldWeapon;
            editorState = EditorState.CANCEL;

            Close();
        }
    }
}
