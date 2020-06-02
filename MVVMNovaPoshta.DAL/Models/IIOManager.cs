using MVVMNovaPoshta.DAL.Context;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMNovaPoshta.DAL.Models
{
    public interface IIOManager
    {
        void Save(ObservableCollection<Delivery> deliveries, string filePath);
        ObservableCollection<Delivery> Load(string filePath);
    }
}
