using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xcom2ClassManager;
using Xcom2ClassManager.FileManagers;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class ClassImporter_UT
    {
        [TestMethod]
        public void TestBasic()
        {
            SoldierClassImporter importer = new SoldierClassImporter();
            string classFile = "D:\\Dev\\X2CM Unit tests\\XComClassData.ini";
            string intFile = "D:\\Dev\\X2CM Unit tests\\XComGame.int";
            string gameFile = "";
            List<SoldierClass> importedClasses = importer.importSoldierClasses(intFile, classFile, gameFile);

            Assert.IsNotNull(importedClasses);
        }
    }
}
