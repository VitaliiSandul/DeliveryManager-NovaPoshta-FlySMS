using MVVMNovaPoshta.BLL.DTO;
using MVVMNovaPoshta.BLL.Infrastructure;
using MVVMNovaPoshta.BLL.Interfaces;
using MVVMNovaPoshta.BLL.Services;
using Newtonsoft.Json;
using NovaPoshtaWpf.Infrastructure;
using NovaPoshtaWpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovaPoshtaWpf.ViewModels
{
    class MainViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<DeliveryDTO> deliveries;
        private DeliveryDTO selectedDelivery;
        private ObservableCollection<CityDTO> cities;
        private CityDTO selectedCity;
        private string infoWindow;
        private NetworkManager manager;
        private IService<DeliveryDTO> deliveryService;

        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand CreateTTNCommand { get; set; }
        public ICommand TrackingCommand { get; set; }
        public ICommand RenewCitiesCommand { get; set; }
        public ICommand SendSMSCommand { get; set; }
        public ICommand ChangeSettingsCommand { get; set; }
        
    #endregion

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

        public ObservableCollection<DeliveryDTO> Deliveries
        {
            get => deliveries;
            set
            {
                deliveries = value;
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

        public string InfoWindow
        {
            get => infoWindow;
            set
            {
                infoWindow = value;
                Notify();
            }
        }
        #endregion

        public MainViewModel()
        {
            string json = File.ReadAllText("CitiesList.json");
            Cities = JsonConvert.DeserializeObject<ObservableCollection<CityDTO>>(json);

            deliveryService = new DeliveryService();
            manager = new NetworkManager();

            Deliveries = new ObservableCollection<DeliveryDTO>(deliveryService.GetAll());
            
            AddCommand = new RelayCommand(AddMethod);
            EditCommand = new RelayCommand(EditMethod);
            RemoveCommand = new RelayCommand(RemoveMethod);            
            SaveCommand = new RelayCommand(SaveMethod);
            LoadCommand = new RelayCommand(LoadMethod);
            CreateTTNCommand = new RelayCommand(CreateTTNMethod);
            TrackingCommand = new RelayCommand(TrackingMethod);
            RenewCitiesCommand = new RelayCommand(RenewCitiesMethod);
            SendSMSCommand = new RelayCommand(SendSMSMethod);
            ChangeSettingsCommand = new RelayCommand(ChangeSettingsMethod);            
        }

        private void AddMethod(object parameter)
        {
            Deliveries.Add(new DeliveryDTO(Deliveries.Last().DeliveryId + 1));
            SelectedDelivery = Deliveries.Last();
            Singleton.getInstance().SelDeliv = SelectedDelivery;
            Singleton.getInstance().CitiesEdit = Cities;
            Singleton.getInstance().SelCity = SelectedCity;
            EditView editView = new EditView();
            editView.ShowDialog();

            try
            {
                deliveryService.CreateOrUpdate(SelectedDelivery);
            }
            catch (Exception ex) { }
        }

        private void EditMethod(object parameter)
        {
            if (SelectedDelivery != null)
            {
                Singleton.getInstance().SelDeliv = SelectedDelivery;
                Singleton.getInstance().CitiesEdit = Cities;
                Singleton.getInstance().SelCity = SelectedCity;
                EditView editView = new EditView();
                editView.ShowDialog();
                try
                {
                    deliveryService.CreateOrUpdate(SelectedDelivery);
                }
                catch(Exception ex) { }                
            }
        }

        private void RemoveMethod(object parameter)
        {
            if (SelectedDelivery != null)
            {
                deliveryService.Delete(SelectedDelivery);
                Deliveries.Remove(SelectedDelivery);
            }
        }

        private void SaveMethod(object parameter)
        {
            try
            {
                deliveryService.Save(Deliveries);
            }
            catch (Exception ex) { }
        }

        private void LoadMethod(object parameter)
        {
            try
            {
                Deliveries = new ObservableCollection<DeliveryDTO>(deliveryService.GetAll());
            }
            catch (Exception ex) { }            
        }

        private async void CreateTTNMethod(object parameter)
        {
            if (SelectedDelivery != null)
            {
                TtnInfo ttn = await manager.CreateTtnAsync(SelectedDelivery);
                SelectedDelivery.Ttn = ttn.IntDocNumber;
                SelectedDelivery.DateArrival = ttn.EstimatedDeliveryDate;
                deliveryService.CreateOrUpdate(SelectedDelivery);
            }
        }

        private async void TrackingMethod(object parameter)
        {
            if (SelectedDelivery != null)
            {
                InfoWindow = await manager.TrackingAsync(SelectedDelivery.Ttn);                  
            }
        }

        private async void RenewCitiesMethod(object parameter)
        {
            Cities = await manager.GetCitiesAsync();
        }

        private async void SendSMSMethod(object parameter)
        {
            if (SelectedDelivery != null)
            {
                InfoWindow = await manager.SendSmsFlyAsync(SelectedDelivery);
            }
        }

        private void ChangeSettingsMethod(object parameter)
        {
            SettingsView settingsView = new SettingsView();
            settingsView.ShowDialog();
        }

        
    }
}
