using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IEntityService<T>
    {
        void ClearFileData();
        T GetData();
        void SerializeData(T obj);
        void AddNewData(T obj);
    }
}
