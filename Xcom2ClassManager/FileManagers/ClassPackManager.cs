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
    public static class ClassPackManager
    {
        public static void saveClassPack(ClassPack classPack, Stream stream)
        {
            if (stream is FileStream fileStream)
            {
                classPack.filePath = fileStream.Name;
            }

            XmlSerializer xs = new XmlSerializer(typeof(ClassPack));
            stream.SetLength(0);
            
            TextWriter tw = new StreamWriter(stream);
            xs.Serialize(tw, classPack);
        }

        public static ClassPack loadClassPack(Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(ClassPack));
            ClassPack classPack;
            using (var sr = new StreamReader(stream))
            {
                classPack = (ClassPack)xs.Deserialize(sr);
            }

            if (stream is FileStream fileStream)
            {
                classPack.filePath = fileStream.Name;
            }

            return classPack;
        }
    }
}
