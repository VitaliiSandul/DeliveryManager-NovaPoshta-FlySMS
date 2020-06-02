using MVVMNovaPoshta.BLL.DTO;
using MVVMNovaPoshta.BLL.Interfaces;
using MVVMNovaPoshta.DAL.Context;
using MVVMNovaPoshta.DAL.Interfaces;
using MVVMNovaPoshta.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Services
{
    public class DeliveryService : IService<DeliveryDTO>
    {
        IUnitOfWork unitOfWork;

        public DeliveryService()
        {
            unitOfWork = new UnitOfWork();
        }

        public void CreateOrUpdate(DeliveryDTO obj)
        {
            Delivery tmp = unitOfWork.DeliveryRepository.GetAll().FirstOrDefault(x => x.DeliveryId == obj.DeliveryId);

            if (tmp != null)
            {
                tmp.DeliveryId = obj.DeliveryId;
                tmp.DeliveryDescription = obj.DeliveryDescription;
                tmp.DateOper = obj.DateOper;
                tmp.Price = obj.Price;
                tmp.CustomerPhone = obj.CustomerPhone;
                tmp.CustomerName = obj.CustomerName;
                tmp.City = obj.City;
                tmp.NumStorage = obj.NumStorage;
                tmp.WeightMax = obj.WeightMax;
                tmp.TTN = obj.Ttn;
                tmp.DateArrival = obj.DateArrival;
            }
            else
            {
                tmp = new Delivery
                {
                    DeliveryId = obj.DeliveryId,
                    DeliveryDescription = obj.DeliveryDescription,
                    DateOper = obj.DateOper,
                    Price = obj.Price,
                    CustomerPhone = obj.CustomerPhone,
                    CustomerName = obj.CustomerName,
                    City = obj.City,
                    NumStorage = obj.NumStorage,
                    WeightMax = obj.WeightMax,
                    TTN = obj.Ttn,
                    DateArrival = obj.DateArrival
            };
            }

            unitOfWork.DeliveryRepository.AddOrUpdate(tmp);
            unitOfWork.Save();
        }

        public void Delete(DeliveryDTO obj)
        {
            Delivery tmp = unitOfWork.DeliveryRepository.GetAll().FirstOrDefault(x => x.DeliveryId == obj.DeliveryId);

            if (tmp != null)
            {
                unitOfWork.DeliveryRepository.Delete(tmp);
            }

            unitOfWork.Save();
        }

        public DeliveryDTO Get(int id)
        {
            var tmp = unitOfWork.DeliveryRepository.Get(id);

            return new DeliveryDTO(tmp.DeliveryId,
                                    tmp.DeliveryDescription,
                                    tmp.DateOper,
                                    tmp.Price,
                                    tmp.CustomerPhone,
                                    tmp.CustomerName,
                                    tmp.City,
                                    tmp.NumStorage,
                                    tmp.WeightMax,
                                    tmp.TTN,
                                    tmp.DateArrival);
        }

        public IEnumerable<DeliveryDTO> GetAll()
        {
            return unitOfWork.DeliveryRepository.GetAll().Select(x => new DeliveryDTO
            {
                DeliveryId = x.DeliveryId,
                DeliveryDescription = x.DeliveryDescription,
                DateOper = x.DateOper,
                Price = x.Price,
                CustomerPhone = x.CustomerPhone,
                CustomerName = x.CustomerName,
                City = x.City,
                NumStorage = x.NumStorage,
                WeightMax = x.WeightMax,
                Ttn = x.TTN,
                DateArrival = x.DateArrival
            }).ToList();
        }

        public void Save(IEnumerable<DeliveryDTO> deliveries = null)
        {
            unitOfWork.Save();
        }
    }
}
