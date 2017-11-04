namespace Xcom2ClassManager
{
    public class Ability
    {
        public string internalName { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public WeaponSlot weaponSlot { get; set; }
        public string requiredMod { get; set; }

        public Ability()
        {
            internalName = "";
            displayName = "";
            description = "";
            weaponSlot = WeaponSlot.None;
            requiredMod = "";
        }

        public Ability(Ability other)
        {
            internalName = other.internalName;
            displayName = other.displayName;
            description = other.description;
            weaponSlot = other.weaponSlot;
            requiredMod = other.requiredMod;
        }

        // Holding in case things break
        //public override bool Equals(object obj)
        //{
        //    Ability castedObj = obj as Ability;

        //    if(castedObj == null)
        //    {
        //        return base.Equals(obj);
        //    }

        //    return (internalName.Equals(castedObj.internalName));
        //}

        public override bool Equals(object obj)
        {
            Ability other = obj as Ability;
            if (other == null)
            {
                return false;
            }

            if (internalName != other.internalName)
            {
                return false;
            }

            if (displayName != other.displayName)
            {
                return false;
            }

            if (description != other.description)
            {
                return false;
            }

            if (weaponSlot != other.weaponSlot)
            {
                return false;
            }

            if (requiredMod != other.requiredMod)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return internalName;
        }

        public string[] getListViewStringArray()
        {
            return new string[2] { internalName, displayName };
        }
    }
}