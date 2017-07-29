using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xcom2ClassManager.FileManagers;

namespace Xcom2ClassManager.DataObjects
{
    public class ProjectState
    {
        private static ProjectState instance;
        private const string DEFAULT_CLASS_PACK_PATH = "";

        private List<Ability> abilities;
        private ClassPack classPack;
        private SoldierClass openSoldierClass;

        private ProjectState()
        {
            AbilityManager abilityManager = new AbilityManager();
            abilities = abilityManager.read();

            //classPack = ClassPackManager.loadClassPack(DEFAULT_CLASS_PACK_PATH);
        }

        private static ProjectState getInstance()
        {
            if(instance == null)
            {
                instance = new ProjectState();
            }

            return instance;
        }

        public static List<Ability> getAbilities()
        {
            return getInstance().abilities;
        }

        public static ClassPack getClassPack()
        {
            return getInstance().classPack;
        }

        public static void setClassPack(ClassPack classPack)
        {
            getInstance().classPack = classPack;
        }

        public static SoldierClass getOpenSoldierClass()
        {
            return getInstance().openSoldierClass;
        }

        public static void setOpenSoldierClass(SoldierClass soldierClass)
        {
            getInstance().openSoldierClass = soldierClass;
        }
    }
}
