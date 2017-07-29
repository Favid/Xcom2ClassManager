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
}
