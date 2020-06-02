using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Infrastructure
{
    public class TtnInfo
    {
        public string TtnRef { get; set; }
        public int CostOnSite { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string IntDocNumber { get; set; }
        public string TypeDocument { get; set; }
    }
}
