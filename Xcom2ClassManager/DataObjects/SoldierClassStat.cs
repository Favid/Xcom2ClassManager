namespace Xcom2ClassManager
{
    public class SoldierClassStat
    {
        public Stat stat { get; set; }
        public int? value { get; set; }
        public SoldierRank rank { get; set; }

        public SoldierClassStat()
        {
            stat = Stat.HP;
            value = 0;
            rank = SoldierRank.Squaddie;
        }

        public SoldierClassStat(SoldierClassStat other)
        {
            stat = other.stat;
            value = other.value;
            rank = other.rank;
        }

        public override bool Equals(object obj)
        {
            SoldierClassStat other = obj as SoldierClassStat;
            if (other == null)
            {
                return false;
            }

            if (stat != other.stat)
            {
                return false;
            }

            if (value != other.value)
            {
                return false;
            }

            if (rank != other.rank)
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