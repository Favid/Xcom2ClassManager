namespace Xcom2ClassManager
{
    public class SoldierClassAbility : Ability
    {
        public SoldierRank rank { get; set; }
        public int? slot { get; set; }

        public SoldierClassAbility() : base()
        {
            rank = SoldierRank.Squaddie;
            slot = 1;
        }

        public SoldierClassAbility(Ability ability) : base(ability)
        {
            rank = SoldierRank.Squaddie;
            slot = 1;
        }

        public SoldierClassAbility(SoldierClassAbility other) : base(other)
        {
            rank = other.rank;
            slot = other.slot;
        }

        public Ability getAbility()
        {
            Ability ability = new Ability();
            ability.internalName = internalName;
            ability.displayName = displayName;
            ability.description = description;
            ability.requiredMod = requiredMod;
            ability.weaponSlot = weaponSlot;

            return ability;
        }

        public override bool Equals(object obj)
        {
            SoldierClassAbility other = obj as SoldierClassAbility;
            if (other == null)
            {
                return false;
            }

            if (rank != other.rank)
            {
                return false;
            }

            if (slot != other.slot)
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}