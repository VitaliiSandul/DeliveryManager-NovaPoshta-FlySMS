using MVVMNovaPoshta.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Infrastructure
{
    [Serializable]
    public class ProgramConfig
    {
        public string ApiKeyNP { get; set; }
        public string LoginFly { get; set; }
        public string PasswordFly { get; set; }
        public Counterparty CurCounterparty { get; set; }
        public CounterpartyContactPerson CurContactPerson { get; set; }
        public CityDTO CurСity { get; set; }
        public WarehouseDTO CurWarehouse { get; set; }

        public ProgramConfig() { }
    }
}
