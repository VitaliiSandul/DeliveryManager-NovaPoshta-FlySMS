using MVVMNovaPoshta.DAL.Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.DAL.Models
{
    public class JsonManager : IIOManager
    {
        public ObservableCollection<Delivery> Load(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ObservableCollection<Delivery>>(json);
        }

        public void Save(ObservableCollection<Delivery> deliveries, string filePath)
        {
            string json = JsonConvert.SerializeObject(deliveries, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
