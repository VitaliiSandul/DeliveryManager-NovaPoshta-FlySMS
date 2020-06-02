using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Infrastructure
{
    [Serializable]
    public class CounterpartyContactPerson
    {
        public string Description { get; set; }
        public string RefContactPerson { get; set; }
        public string Phones { get; set; }
    }
}
