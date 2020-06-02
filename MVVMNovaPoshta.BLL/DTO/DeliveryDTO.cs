using MVVMNovaPoshta.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.DTO
{
    public class DeliveryDTO : BaseNotifyPropertyChanged
    {
        private int deliveryId;
        private string deliveryDescription;
        private DateTime? dateOper;
        private decimal price;
        private string customerPhone;
        private string customerName;
        private string city;
        private int? numStorage;
        private double? weightMax;
        private string ttn;
        private DateTime? dateArrival;

        #region Constructor
        public DeliveryDTO(int deliveryId, string deliveryDescription, DateTime? dateOper, decimal price, string customerPhone, string customerName, string city, int? numStorage, double? weightMax, string ttn, DateTime? dateArrival)
        {
            this.deliveryId = deliveryId;
            this.deliveryDescription = deliveryDescription;
            this.dateOper = dateOper;
            this.price = price;
            this.customerPhone = customerPhone;
            this.customerName = customerName;
            this.city = city;
            this.numStorage = numStorage;
            this.weightMax = weightMax;
            this.ttn = ttn;
            this.dateArrival = dateArrival;
        }

        public DeliveryDTO(int id)
        {
            DeliveryId = id;
            DeliveryDescription = "";
            DateOper = null;
            Price = 0;
            CustomerPhone = "";
            CustomerName = "";
            City = "";
            NumStorage = null;
            WeightMax = 0;
            Ttn = "";
            DateArrival = null;
        }

        public DeliveryDTO()
        {
            DeliveryId = 0;
            DeliveryDescription = "";
            DateOper = null;
            Price = 0;
            CustomerPhone = "";
            CustomerName = "";
            City = "";
            NumStorage = null;
            WeightMax = 0;
            Ttn = "";
            DateArrival = null;
        }
        #endregion

        #region Properties

        public int DeliveryId
        {
            get => deliveryId;
            set
            {
                deliveryId = value;
                Notify();
            }
        }

        public string DeliveryDescription
        {
            get => deliveryDescription;
            set
            {
                deliveryDescription = value;
                Notify();
            }
        }

        public DateTime? DateOper
        {
            get => dateOper;
            set
            {
                dateOper = value;
                Notify();
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                Notify();
            }
        }

        public string CustomerPhone
        {
            get => customerPhone;
            set
            {
                customerPhone = value;
                Notify();
            }
        }

        public string CustomerName
        {
            get => customerName;
            set
            {
                customerName = value;
                Notify();
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;
                Notify();
            }
        }

        public int? NumStorage
        {
            get => numStorage;
            set
            {
                numStorage = value;
                Notify();
            }
        }

        public double? WeightMax
        {
            get => weightMax;
            set
            {
                weightMax = value;
                Notify();
            }
        }

        public string Ttn
        {
            get => ttn;
            set
            {
                ttn = value;
                Notify();
            }
        }

        public DateTime? DateArrival
        {
            get => dateArrival;
            set
            {
                dateArrival = value;
                Notify();
            }
        }
        #endregion
    }
}
