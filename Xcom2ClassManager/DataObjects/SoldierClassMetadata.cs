using System;
using System.Collections.Generic;
using System.Linq;

namespace Xcom2ClassManager
{
    public class SoldierClassMetadata
    {
        public string internalName { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string iconString { get; set; }
        public bool allowBonds { get; set; }
        public List<string> unfavoredClasses { get; set; }

        public SoldierClassMetadata()
        {
            internalName = "";
            displayName = "";
            description = "";
            iconString = "";
            allowBonds = false;
            unfavoredClasses = new List<string>();
        }

        public SoldierClassMetadata(SoldierClassMetadata other)
        {
            internalName = other.internalName;
            displayName = other.displayName;
            description = other.description;
            iconString = other.iconString;
            allowBonds = other.allowBonds;

            unfavoredClasses = new List<string>();
            foreach (string unfavoredClass in other.unfavoredClasses)
            {
                unfavoredClasses.Add(unfavoredClass);
            }
        }

        public override string ToString()
        {
            return internalName;
        }

        public override bool Equals(object obj)
        {
            SoldierClassMetadata other = obj as SoldierClassMetadata;
            if (other == null)
            {
                return false;
            }

            if (!string.Equals(internalName, other.internalName))
            {
                return false;
            }

            if (!string.Equals(displayName, other.displayName))
            {
                return false;
            }

            if (!string.Equals(description, other.description))
            {
                return false;
            }

            if (!string.Equals(iconString, other.iconString))
            {
                return false;
            }

            if (allowBonds != other.allowBonds)
            {
                return false;
            }
            
            if (unfavoredClasses.Count() != other.unfavoredClasses.Count())
            {
                return false;
            }

            for (int i = 0; i < unfavoredClasses.Count(); i++)
            {
                if (unfavoredClasses[i] != other.unfavoredClasses[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}