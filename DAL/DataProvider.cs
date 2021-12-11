using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class DataProvider<T> : IDataProvider<T>
    {
        public string FilePath { get; set; }

        public DataProvider(string path)
        {
            FilePath = path;
        }

        public void DeleteFileData()
        {
            File.WriteAllText(FilePath, string.Empty);
        }
        public bool FileExists()
        {
            if (File.Exists(FilePath))
            {
                return true;
            }
            return false;
        }

        public bool FileHasData()
        {
            if (FileExists())
            {
                if (File.ReadAllText(FilePath) != string.Empty)
                    return true;
            }
            return false;
        }

        public void DeleteFile()
        {
            File.Delete(FilePath);
        }
        public abstract T Deserialize();
        public abstract void Serialize(T obj);
    }
}
