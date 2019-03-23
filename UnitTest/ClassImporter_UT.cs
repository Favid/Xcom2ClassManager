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
            List<SoldierClass> actual = importer.importSoldierClasses(intFile, classFile, gameFile, "Unit Test", out List<Ability> test);
            
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

        private void AssertSoldierClassesEqual(SoldierClass expected, SoldierClass actual)
        {
            Assert.AreEqual(expected.metadata, actual.metadata);
            Assert.AreEqual(expected.allowAwcAbilities, actual.allowAwcAbilities);
            Assert.AreEqual(expected.baseAbilityPointsPerPromotion, actual.baseAbilityPointsPerPromotion);
            Assert.AreEqual(expected.experience, actual.experience);
            Assert.AreEqual(expected.equipment, actual.equipment);
            Assert.AreEqual(expected.leftTreeName, actual.leftTreeName);
            Assert.AreEqual(expected.middleTreeName, actual.middleTreeName);
            Assert.AreEqual(expected.rightTreeName, actual.rightTreeName);

            Assert.AreEqual(expected.awcExcludeAbilities.Count(), actual.awcExcludeAbilities.Count());
            for(int i = 0; i < expected.awcExcludeAbilities.Count(); i++)
            {
                Assert.AreEqual(expected.awcExcludeAbilities[i], actual.awcExcludeAbilities[i]);
            }

            Assert.AreEqual(expected.loadoutItems.Count(), actual.loadoutItems.Count());
            for (int i = 0; i < expected.loadoutItems.Count(); i++)
            {
                Assert.AreEqual(expected.loadoutItems[i], actual.loadoutItems[i]);
            }

            Assert.AreEqual(expected.stats.Count(), actual.stats.Count());
            for (int i = 0; i < expected.stats.Count(); i++)
            {
                Assert.AreEqual(expected.stats[i], actual.stats[i]);
            }

            Assert.AreEqual(expected.nicknames.Count(), actual.nicknames.Count());
            for (int i = 0; i < expected.nicknames.Count(); i++)
            {
                Assert.AreEqual(expected.nicknames[i], actual.nicknames[i]);
            }

            Assert.AreEqual(expected.soldierAbilities.Count(), actual.soldierAbilities.Count());
            for (int i = 0; i < expected.soldierAbilities.Count(); i++)
            {
                Assert.AreEqual(expected.soldierAbilities[i], actual.soldierAbilities[i]);
            }
        }
    }
}
