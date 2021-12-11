using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDataProvider<T>
    {
        string FilePath { get; set; }
        void DeleteFileData();
        bool FileExists();
        bool FileHasData();
        void DeleteFile();
        void Serialize(T obj);
        T Deserialize();
    }
}
