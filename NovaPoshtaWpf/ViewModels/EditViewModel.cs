using MVVMNovaPoshta.BLL.DTO;
using NovaPoshtaWpf.Infrastructure;
using MVVMNovaPoshta.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace NovaPoshtaWpf.ViewModels
{
    class EditViewModel : BaseNotifyPropertyChanged
    {
        private DeliveryDTO selectedDelivery;
        private ObservableCollection<CityDTO> cities;
        private CityDTO selectedCity;
        private ObservableCollection<WarehouseDTO> warehouses;
        private WarehouseDTO selectedWarehouse;
        private int selectedNumStorage;
        private NetworkManager managerWh;

        #region Properties
        public DeliveryDTO SelectedDelivery
        {
            get => selectedDelivery;
            set
            {
                selectedDelivery = value;
                Notify();
            }
        }

        public CityDTO SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                Notify();
            }
        }

        public ObservableCollection<CityDTO> Cities
        {
            get => cities;
            set
            {
                cities = value;
                Notify();
            }
        }

        public ObservableCollection<WarehouseDTO> Warehouses
        {
            get => warehouses;
            set
            {
                warehouses = value;
                Notify();
            }
        }

        public WarehouseDTO SelectedWarehouse
        {
            get => selectedWarehouse;
            set
            {
                selectedWarehouse = value;
                Notify();
            }
        }

        public int SelectedNumStorage
        {
            get => selectedNumStorage;
            set
            {
                selectedNumStorage = value;
                Notify();
            }
        }
        #endregion

        public ICommand ChangeCityCommand { get; set; }
        public ICommand ChangeWarehouseCommand { get; set; }

        public EditViewModel()
        {
            managerWh = new NetworkManager();
            ChangeCityCommand = new RelayCommand(ChangeCityMethod);
            ChangeWarehouseCommand = new RelayCommand(ChangeWarehouseMethod);
                        
            SelectedDelivery = Singleton.getInstance().SelDeliv;
            Cities = Singleton.getInstance().CitiesEdit;
            SelectedCity = Singleton.getInstance().SelCity;                      
        }

        private async void ChangeCityMethod(object parameter)
        {
            selectedDelivery.City = SelectedCity.Description;            
            Warehouses = await managerWh.GetWarehousesAsync(selectedDelivery.City);
        }

        private void ChangeWarehouseMethod(object parameter)
        {
            try
            {
                selectedDelivery.NumStorage = SelectedWarehouse.Number;
            }
            catch(Exception ex) { }            
        }


    }
}
