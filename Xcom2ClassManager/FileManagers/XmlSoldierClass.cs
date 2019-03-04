using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xcom2ClassManager.DataObjects;

namespace Xcom2ClassManager.FileManagers
{
    public class XmlSoldierClasss
    {
        public struct XmlSoldierAbility
        {
            public int id { get; set; }
            public string internalName { get; set; }
            public SoldierRank rank { get; set; }
            public int? slot { get; set; }

            public XmlSoldierAbility(XmlSoldierAbility other)
            {
                id = other.id;
                internalName = other.internalName;
                rank = other.rank;
                slot = other.slot;
            }

            public XmlSoldierAbility(SoldierClassAbility other)
            {
                id = other.id;
                internalName = other.internalName;
                rank = other.rank;
                slot = other.slot;
            }
        };
        
        public SoldierClassMetadata metadata { get; set; }
        public SoldierClassExperience experience { get; set; }
        public SoldierClassEquipment equipment { get; set; }
        public List<XmlSoldierAbility> xmlSoldierAbilities { get; set; }
        public List<SoldierClassStat> stats { get; set; }

        public string leftTreeName { get; set; }
        public string rightTreeName { get; set; }

        public List<ClassNickname> nicknames { get; set; }
        public List<string> loadoutItems { get; set; }

        public bool allowAwcAbilities { get; set; }
        public int baseAbilityPointsPerPromotion { get; set; }
        public List<Ability> awcExcludeAbilities { get; set; }
        
        public XmlSoldierClasss()
        {
            metadata = new SoldierClassMetadata();
            experience = new SoldierClassExperience();
            equipment = new SoldierClassEquipment();
            xmlSoldierAbilities = new List<XmlSoldierAbility>();
            stats = new List<SoldierClassStat>();

            leftTreeName = "";
            rightTreeName = "";

            nicknames = new List<ClassNickname>();
            loadoutItems = new List<string>();

            allowAwcAbilities = true;
            baseAbilityPointsPerPromotion = 4;
            awcExcludeAbilities = new List<Ability>();
        }

        public XmlSoldierClasss(XmlSoldierClasss other)
        {
            metadata = new SoldierClassMetadata(other.metadata);
            experience = new SoldierClassExperience(other.experience);
            equipment = new SoldierClassEquipment(other.equipment);

            xmlSoldierAbilities = new List<XmlSoldierAbility>();
            foreach (XmlSoldierAbility soldierAbility in other.xmlSoldierAbilities)
            {
                xmlSoldierAbilities.Add(new XmlSoldierAbility(soldierAbility));
            }

            stats = new List<SoldierClassStat>();
            foreach (SoldierClassStat stat in other.stats)
            {
                stats.Add(new SoldierClassStat(stat));
            }

            leftTreeName = other.leftTreeName;
            rightTreeName = other.rightTreeName;

            nicknames = new List<ClassNickname>();
            foreach (ClassNickname nickname in other.nicknames)
            {
                nicknames.Add(new ClassNickname(nickname));
            }

            loadoutItems = new List<string>();
            foreach (string loadoutItem in other.loadoutItems)
            {
                loadoutItems.Add(loadoutItem);
            }

            allowAwcAbilities = other.allowAwcAbilities;
            baseAbilityPointsPerPromotion = other.baseAbilityPointsPerPromotion;

            awcExcludeAbilities = new List<Ability>();
            foreach (Ability excludeAbility in other.awcExcludeAbilities)
            {
                awcExcludeAbilities.Add(excludeAbility);
            }
        }

        public XmlSoldierClasss(SoldierClass other)
        {
            metadata = new SoldierClassMetadata(other.metadata);
            experience = new SoldierClassExperience(other.experience);
            equipment = new SoldierClassEquipment(other.equipment);

            xmlSoldierAbilities = new List<XmlSoldierAbility>();
            foreach (SoldierClassAbility soldierAbility in other.soldierAbilities)
            {
                xmlSoldierAbilities.Add(new XmlSoldierAbility(soldierAbility));
            }

            stats = new List<SoldierClassStat>();
            foreach (SoldierClassStat stat in other.stats)
            {
                stats.Add(new SoldierClassStat(stat));
            }

            leftTreeName = other.leftTreeName;
            rightTreeName = other.rightTreeName;

            nicknames = new List<ClassNickname>();
            foreach (ClassNickname nickname in other.nicknames)
            {
                nicknames.Add(new ClassNickname(nickname));
            }

            loadoutItems = new List<string>();
            foreach (string loadoutItem in other.loadoutItems)
            {
                loadoutItems.Add(loadoutItem);
            }

            allowAwcAbilities = other.allowAwcAbilities;
            baseAbilityPointsPerPromotion = other.baseAbilityPointsPerPromotion;

            awcExcludeAbilities = new List<Ability>();
            foreach (Ability excludeAbility in other.awcExcludeAbilities)
            {
                awcExcludeAbilities.Add(excludeAbility);
            }
        }
    }
}
