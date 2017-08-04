namespace Xcom2ClassManager
{
    public class Weapon
    {
        public string weaponName { get; set; }
        public WeaponSlot weaponSlot { get; set; }

        public Weapon()
        {
            weaponName = "";
            weaponSlot = WeaponSlot.None;
        }

        public Weapon(Weapon other)
        {
            weaponName = other.weaponName;
            weaponSlot = other.weaponSlot;
        }

        public Weapon(string weaponName, WeaponSlot weaponSlot)
        {
            this.weaponName = weaponName;
            this.weaponSlot = weaponSlot;
        }

        public Weapon(string weaponName, string weaponSlot)
        {
            this.weaponName = weaponName;
            this.weaponSlot = Enums.getEnumWithName<WeaponSlot>(weaponSlot);
        }

        public string toString()
        {
            return weaponName + " " + weaponSlot;
        }

        public override bool Equals(object obj)
        {
            Weapon other = obj as Weapon;
            if (other == null)
            {
                return false;
            }

            if (!string.Equals(weaponName, other.weaponName))
            {
                return false;
            }

            if (weaponSlot != other.weaponSlot)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string[] getListViewStringArray()
        {
            return new string[2] { weaponName, weaponSlot.ToString() };
        }
    }
}