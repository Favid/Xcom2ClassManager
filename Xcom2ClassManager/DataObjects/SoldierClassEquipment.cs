using System.Collections.Generic;
using System.Linq;

namespace Xcom2ClassManager
{
    public class SoldierClassEquipment
    {
        public string squaddieLoadout { get; set; }
        public List<string> allowedArmors { get; set; }
        public List<Weapon> weapons { get; set; }

        public SoldierClassEquipment()
        {
            squaddieLoadout = "";
            allowedArmors = new List<string>();
            weapons = new List<Weapon>();
        }

        public SoldierClassEquipment(SoldierClassEquipment other) : base()
        {
            squaddieLoadout = other.squaddieLoadout;
            allowedArmors = other.allowedArmors;

            weapons = new List<Weapon>();
            foreach (Weapon weapon in other.weapons)
            {
                weapons.Add(new Weapon(weapon));
            }
        }

        public override bool Equals(object obj)
        {
            SoldierClassEquipment other = obj as SoldierClassEquipment;
            if (other == null)
            {
                return false;
            }

            if (!string.Equals(squaddieLoadout, other.squaddieLoadout))
            {
                return false;
            }

            if (!allowedArmors.SequenceEqual(other.allowedArmors))
            {
                return false;
            }

            if (!weapons.SequenceEqual(other.weapons))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}