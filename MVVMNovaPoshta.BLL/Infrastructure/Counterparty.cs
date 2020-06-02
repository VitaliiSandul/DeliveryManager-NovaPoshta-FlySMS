using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Infrastructure
{    
    [Serializable]
    public class Counterparty
    {
        public string Description { get; set; }
        public string RefCounterparty { get; set; }
        public string City { get; set; }
    }
}
