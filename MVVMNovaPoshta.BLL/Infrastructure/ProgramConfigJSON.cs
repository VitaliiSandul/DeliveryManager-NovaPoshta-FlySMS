using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.BLL.Infrastructure
{
    public class ProgramConfigJSON
    {
        public ProgramConfig Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ProgramConfig>(json);
        }

        public void Save(ProgramConfig pogramConfig, string filePath)
        {
            string json = JsonConvert.SerializeObject(pogramConfig, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
