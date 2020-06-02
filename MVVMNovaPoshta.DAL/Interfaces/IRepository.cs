using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.DAL.Interfaces
{
    public interface IRepository<T>
    {
        void AddOrUpdate(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
