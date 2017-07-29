﻿using System;

namespace Xcom2ClassManager
{
    public class SoldierClassMetadata
    {
        public string internalName { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string iconString { get; set; }

        public SoldierClassMetadata()
        {
            internalName = "";
            displayName = "";
            description = "";
            iconString = "";
        }

        public SoldierClassMetadata(SoldierClassMetadata other)
        {
            internalName = other.internalName;
            displayName = other.displayName;
            description = other.description;
            iconString = other.iconString;
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

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}