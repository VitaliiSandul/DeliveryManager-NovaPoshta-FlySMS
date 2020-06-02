using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Interfaces
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void CreateOrUpdate(T obj);
        void Delete(T obj);
        void Save(IEnumerable<T> value = null);
    }
}
