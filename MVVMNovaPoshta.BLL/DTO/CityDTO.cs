using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.DTO
{
    [Serializable]
    public class CityDTO
    {
        public string Description { get; set; }
        public string RefCity { get; set; }
        public string Area { get; set; }
        public int CityID { get; set; }
    }
}
