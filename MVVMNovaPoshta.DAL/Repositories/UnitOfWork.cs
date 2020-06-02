using MVVMNovaPoshta.DAL.Context;
using MVVMNovaPoshta.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private NpDbContext context;
        private IRepository<Delivery> deliveryRepository;

        #region Constructor
        public UnitOfWork()
        {
            context = new NpDbContext();
        }
        #endregion

        #region Properties
        public IRepository<Delivery> DeliveryRepository => deliveryRepository = deliveryRepository ?? new DeliveryRepository(context);
        #endregion

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
