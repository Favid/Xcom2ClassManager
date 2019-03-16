using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xcom2ClassManager;
using Xcom2ClassManager.FileManagers;
using System.Collections.Generic;
using System.IO;
using Xcom2ClassManager.DataObjects;
using System.Linq;
using System.ComponentModel;

namespace UnitTest
{
    [TestClass]
    public class ClassImporter_UT
    {
        [TestMethod]
        public void Test_LW2()
        {
            SoldierClassImporter importer = new SoldierClassImporter();
            string classFile = "D:\\Dev\\X2CM Unit tests\\Test_LW2\\XComClassData.ini";
            string intFile = "D:\\Dev\\X2CM Unit tests\\Test_LW2\\XComGame.int";
            string gameFile = "D:\\Dev\\X2CM Unit tests\\Test_LW2\\XComGameData.ini";
            List<SoldierClass> actual = importer.importSoldierClasses(intFile, classFile, gameFile);


            string expectedFile = "D:\\Dev\\X2CM Unit tests\\Test_LW2\\LW2 import.xml";
            FileStream myStream = new FileStream(expectedFile, FileMode.Open);
            ClassPack classPack = ClassPackManager.loadClassPack(myStream);
            BindingList<SoldierClass> expected = classPack.soldierClasses;

            Assert.AreEqual(actual.Count(), expected.Count());
            for (int i = 0; i < actual.Count(); i++)
            {
                AssertSoldierClassesEqual(expected[i], actual[i]);
            }

        }

        private void AssertSoldierClassesEqual(SoldierClass a, SoldierClass b)
        {
            Assert.AreEqual(a.metadata, b.metadata);
            Assert.AreEqual(a.allowAwcAbilities, b.allowAwcAbilities);
            Assert.AreEqual(a.baseAbilityPointsPerPromotion, b.baseAbilityPointsPerPromotion);
            Assert.AreEqual(a.experience, b.experience);
            Assert.AreEqual(a.equipment, b.equipment);
            Assert.AreEqual(a.leftTreeName, b.leftTreeName);
            Assert.AreEqual(a.rightTreeName, b.rightTreeName);

            Assert.AreEqual(a.awcExcludeAbilities.Count(), b.awcExcludeAbilities.Count());
            for(int i = 0; i < a.awcExcludeAbilities.Count(); i++)
            {
                Assert.AreEqual(a.awcExcludeAbilities[i], b.awcExcludeAbilities[i]);
            }

            Assert.AreEqual(a.loadoutItems.Count(), b.loadoutItems.Count());
            for (int i = 0; i < a.loadoutItems.Count(); i++)
            {
                Assert.AreEqual(a.loadoutItems[i], b.loadoutItems[i]);
            }

            Assert.AreEqual(a.stats.Count(), b.stats.Count());
            for (int i = 0; i < a.stats.Count(); i++)
            {
                Assert.AreEqual(a.stats[i], b.stats[i]);
            }

            Assert.AreEqual(a.nicknames.Count(), b.nicknames.Count());
            for (int i = 0; i < a.nicknames.Count(); i++)
            {
                Assert.AreEqual(a.nicknames[i], b.nicknames[i]);
            }

            Assert.AreEqual(a.soldierAbilities.Count(), b.soldierAbilities.Count());
            for (int i = 0; i < a.soldierAbilities.Count(); i++)
            {
                Assert.AreEqual(a.soldierAbilities[i], b.soldierAbilities[i]);
            }

        }
    }
}
