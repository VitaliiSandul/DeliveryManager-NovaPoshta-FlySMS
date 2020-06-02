using MVVMNovaPoshta.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Delivery> DeliveryRepository { get; }

        void Save();
    }
}
