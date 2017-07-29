using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xcom2ClassManager.DataObjects
{
    public class ClassPack
    {
        public string name { get; set; }
        public BindingList<SoldierClass> soldierClasses { get; set; }
        public string filePath { get; set; }

        public ClassPack()
        {
            // TODO let user enter name
            name = "New Class Pack";
            filePath = "";
            soldierClasses = new BindingList<SoldierClass>();

            
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
