using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.DTO
{
    public class WarehouseDTO
    {
        public string Description { get; set; }
        public string WarehouseRef { get; set; }
        public int Number { get; set; }
        public string CityRef { get; set; }
        public string CityDescription { get; set; }        
    }
}
