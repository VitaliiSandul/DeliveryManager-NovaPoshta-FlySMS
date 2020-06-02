using MVVMNovaPoshta.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPoshtaWpf.Infrastructure
{
    public sealed class Singleton
    {
        public DeliveryDTO SelDeliv { get; set; }
        public ObservableCollection<CityDTO> CitiesEdit { get; set; }
        public CityDTO SelCity { get; set; }
        
        private static Singleton instance;

        private Singleton()
        { }

        public static Singleton getInstance()
        {
            if (instance == null)
                instance = new Singleton();
            return instance;
        }
    }
}
