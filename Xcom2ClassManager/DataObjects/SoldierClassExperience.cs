namespace Xcom2ClassManager
{
    public class SoldierClassExperience
    {
        public int numberInForcedDeck { get; set; }
        public int numberInDeck { get; set; }
        public int killAssistsPerKill { get; set; }
        public double missionExperienceWeight { get; set; }

        public SoldierClassExperience()
        {
            numberInForcedDeck = 1;
            numberInDeck = 4;
            killAssistsPerKill = 5;
            missionExperienceWeight = 5.7;
        }

        public SoldierClassExperience(SoldierClassExperience other)
        {
            numberInForcedDeck = other.numberInForcedDeck;
            numberInDeck = other.numberInDeck;
            killAssistsPerKill = other.killAssistsPerKill;
            missionExperienceWeight = other.missionExperienceWeight;
        }

        public override bool Equals(object obj)
        {
            SoldierClassExperience other = obj as SoldierClassExperience;
            if (other == null)
            {
                return false;
            }

            if (numberInForcedDeck != other.numberInForcedDeck)
            {
                return false;
            }

            if (numberInDeck != other.numberInDeck)
            {
                return false;
            }

            if (killAssistsPerKill != other.killAssistsPerKill)
            {
                return false;
            }

            if (missionExperienceWeight != other.missionExperienceWeight)
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