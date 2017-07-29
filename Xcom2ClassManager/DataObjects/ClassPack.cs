using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.DataObjects
{
    public class ClassPack
    {
        public string name { get; set; }
        public List<SoldierClass> soldierClasses { get; set; }

        public ClassPack()
        {
            // TODO let user enter name
            name = "New Class Pack";

            soldierClasses = new List<SoldierClass>();

            // TODO once the overview form has a state for no loaded class, remove this
            SoldierClass soldierClass = new SoldierClass();
            soldierClass.metadata.internalName = "NewClass";

            soldierClasses.Add(soldierClass);
        }

        public ClassPack(ClassPack other)
        {
            this.name = other.name;
            foreach (SoldierClass soldierClass in other.soldierClasses)
            {
                this.soldierClasses.Add(new SoldierClass(soldierClass));
            }
        }


    }
}
