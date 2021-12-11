using System;
using System.IO;
using System.Xml.Serialization;

namespace DAL
{
    public class XMLProvider<T> : DataProvider<T>
    {
        XmlSerializer xmlFormatter;
        
        public XMLProvider(string path)
            : base(path)
        {
            xmlFormatter = new XmlSerializer(typeof(T));
        }

        public override void Serialize(T obj)
        {
            // if (FileExists()) { throw new Exception("Заданий файл вже існує!"); }
            using (var file = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(file, obj);
            }
        }

        public override T Deserialize()
        {
            if (FileExists() == false) { throw new Exception("Немає даних для десеріалізації"); }

            using (var file = new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                return (T)xmlFormatter.Deserialize(file);
            }
        }
    }
}
