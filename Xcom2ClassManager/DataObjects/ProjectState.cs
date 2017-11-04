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
        }

        private static ProjectState getInstance()
        {
            if(instance == null)
            {
                instance = new ProjectState();
            }

            return instance;
        }

        public static void reloadAbilities()
        {
            AbilityManager abilityManager = new AbilityManager();
            instance.abilities = abilityManager.read();
        }

        public static List<Ability> getAbilities()
        {
            return getInstance().abilities;
        }

        public static Ability getAbility(int id)
        {
            Ability ability = instance.abilities.Where(x => x.id.Equals(id)).FirstOrDefault();
            return ability;
        }

        public static ClassPack getClassPack()
        {
            return getInstance().classPack;
        }

        public static void setClassPack(ClassPack classPack)
        {
            getInstance().classPack = classPack;
        }

        public static SoldierClass addNewClassPackSoldierClass()
        {
            SoldierClass soldierClass = new SoldierClass();
            soldierClass.metadata.internalName = generateNewClassName("NewClass");
            instance.classPack.soldierClasses.Add(soldierClass);
            return soldierClass;
        }

        public static SoldierClass copyOpenSoldierClass()
        {
            SoldierClass classToCopy = ProjectState.getOpenSoldierClass();
            SoldierClass newClass = new SoldierClass(classToCopy);
            newClass.metadata.internalName = generateNewClassName(classToCopy.metadata.internalName);
            
            instance.classPack.soldierClasses.Add(newClass);
            return newClass;
        }

        private static string generateNewClassName(string baseName)
        {
            string name = baseName;
            int appendedNumber = 1;

            while(getInstance().classPack.soldierClasses.Where(x => x.metadata.internalName.Equals(name)).Count() > 0)
            {
                name = baseName + appendedNumber.ToString();
                appendedNumber++;
            }

            return name;
        }

        internal static int getNextAbilityId()
        {
            return instance.abilities.Max(x => x.id) + 1;
        }

        public static void renameOpenClass(string newName)
        {
            SoldierClass classToUpdate = getOpenSoldierClass();
            int indexToReplace = getClassPack().soldierClasses.IndexOf(classToUpdate);
            instance.classPack.soldierClasses[indexToReplace].metadata.internalName = newName;
        }

        public static void updateClassPackSoldierClass(string internalName, SoldierClass soldierClass)
        {
            SoldierClass classToReplace = getClassPack().soldierClasses.Where(x => x.metadata.internalName.Equals(internalName)).FirstOrDefault();
            if(classToReplace != null)
            {
                int indexToReplace = getClassPack().soldierClasses.IndexOf(classToReplace);
                instance.classPack.soldierClasses[indexToReplace] = soldierClass;
            }
            
        }

        public static SoldierClass getOpenSoldierClass()
        {
            return getInstance().openSoldierClass;
        }

        public static void setOpenSoldierClass(SoldierClass soldierClass)
        {
            getInstance().openSoldierClass = soldierClass;
        }

        public static void deleteOpenSoldierClass()
        {
            instance.classPack.soldierClasses.Remove(getOpenSoldierClass());
        }
    }
}
