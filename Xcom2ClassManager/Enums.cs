using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager
{
    public enum WeaponSlot
    {
        [Description("")]
        None = 0,

        [Description("eInvSlot_Unknown")]
        Unknown = 1,

        [Description("eInvSlot_PrimaryWeapon")]
        Primary = 2,

        [Description("eInvSlot_SecondaryWeapon")]
        Secondary = 3,

        [Description("eInvSlot_HeavyWeapon")]
        Heavy = 4
    }

    public enum SoldierRank
    {
        [Description("Rookie")]
        Rookie = 0,

        [Description("Squaddie")]
        Squaddie = 1,

        [Description("Corporal")]
        Corporal = 2,

        [Description("Sergeant")]
        Sergeant = 3,

        [Description("Lieutenant")]
        Lieutenant = 4,

        [Description("Captain")]
        Captain = 5,

        [Description("Major")]
        Major = 6,

        [Description("Colonel")]
        Colonel = 7,

        [Description("Brigadier")]
        Brigadier = 8
    }

    public enum Stat
    {
        [Description("eStat_HP")]
        HP = 0,

        [Description("eStat_Offense")]
        Aim = 1,

        [Description("eStat_Strength")]
        Strength = 2,

        [Description("eStat_Hacking")]
        Hacking = 3,

        [Description("eStat_PsiOffense")]
        Psi = 4,

        [Description("eStat_Mobility")]
        Mobility = 5,

        [Description("eStat_Will")]
        Will = 6,

        [Description("eStat_Dodge")]
        Dodge = 7
    }

    public enum EditorState
    {
        ADD,
        EDIT,
        CANCEL
    }

    public enum NicknameGender
    {
        [Description("RandomNickNames")]
        Unisex = 0,

        [Description("RandomNicknames_Male")]
        Male = 1,

        [Description("RandomNicknames_Female")]
        Female = 2
    }

    public static class Enums
    {
        public static string getDescription(Enum enumValue)
        {
            DescriptionAttribute description = Enums.getAttributeOfType<DescriptionAttribute>(enumValue);
            return description.Description;
        }

        private static T getAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static T getEnumWithName<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
    }

    
}
