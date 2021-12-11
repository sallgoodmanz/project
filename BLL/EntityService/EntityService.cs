using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class EntityService<T> : IEntityService<T>
    {
        private IDataProvider<T> provider;
        public IDataProvider<T> Provider
        {
            get { return provider; }
            set
            {
                if (value == null) { throw new NullReferenceException(); }
                provider = value;
            }
        }
        public EntityService(IDataProvider<T> provider)
        {
            if (provider == null) { throw new NullReferenceException(); }

            this.provider = provider;
        }
       
        public void ClearFileData() { provider.DeleteFileData(); }
        public T GetData() { return provider.Deserialize(); }
        public void SerializeData(T obj) { provider.Serialize(obj); }
        public void AddNewData(T obj) { ClearFileData(); SerializeData(obj); }
    }
}
