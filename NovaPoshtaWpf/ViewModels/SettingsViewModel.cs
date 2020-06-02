using MVVMNovaPoshta.BLL.DTO;
using MVVMNovaPoshta.BLL.Infrastructure;
using Newtonsoft.Json;
using NovaPoshtaWpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace NovaPoshtaWpf.ViewModels
{
    class SettingsViewModel : BaseNotifyPropertyChanged
    {
        private ProgramConfig config;
        private ProgramConfigJSON programConfigJSON;
        private ObservableCollection<Counterparty> counterparties;
        private Counterparty selectedCounterparty;
        private ObservableCollection<CounterpartyContactPerson> contactPersons;
        private CounterpartyContactPerson selectedContactPerson;
        private ObservableCollection<CityDTO> cities;
        private ObservableCollection<WarehouseDTO> warehouses;
        private WarehouseDTO selectedWarehouse;
        private CityDTO selectedCity;
        private string tmpApiKeyNP;
        private string tmpLoginFly;
        private string tmpPasswordFly;
        private Counterparty tmpCounterparty;
        private CounterpartyContactPerson tmpContactPerson;
        private CityDTO tmpCity;
        private WarehouseDTO tmpWarehouse;
        private NetworkManager manager;
               
        #region Properties
        public Counterparty SelectedCounterparty
        {
            get => selectedCounterparty;
            set
            {
                selectedCounterparty = value;
                Notify();
            }
        }

        public ObservableCollection<Counterparty> Counterparties
        {
            get => counterparties;
            set
            {
                counterparties = value;
                Notify();
            }
        }

        public CounterpartyContactPerson SelectedContactPerson
        {
            get => selectedContactPerson;
            set
            {
                selectedContactPerson = value;
                Notify();
            }
        }

        public ObservableCollection<CounterpartyContactPerson> ContactPersons
        {
            get => contactPersons;
            set
            {
                contactPersons = value;
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

        public ProgramConfig Config
        {
            get => config;
            set
            {
                config = value;
                Notify();
            }
        }
        #endregion

        public ICommand GetCounterpartiesCommand { get; set; }
        public ICommand GetContactPersonsCommand { get; set; }
        public ICommand GetCitiesCommand { get; set; }
        public ICommand GetWarehousesCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveSettingsCommand { get; set; }
        public ICommand ChangeCounterpartyCommand { get; set; }
        public ICommand ChangeContactPersonCommand { get; set; }
        public ICommand ChangeCityCommand { get; set; }
        public ICommand ChangeWarehouseCommand { get; set; }

        public SettingsViewModel()
        {
            manager = new NetworkManager();
            config = new ProgramConfig();
            programConfigJSON = new ProgramConfigJSON();
            Config = programConfigJSON.Load("programConfig.json");

            Counterparties = new ObservableCollection<Counterparty>();
            SelectedCounterparty = Config.CurCounterparty;
            Counterparties.Add(SelectedCounterparty);

            ContactPersons = new ObservableCollection<CounterpartyContactPerson>();
            SelectedContactPerson = Config.CurContactPerson;
            ContactPersons.Add(SelectedContactPerson);

            Cities = new ObservableCollection<CityDTO>();
            SelectedCity = Config.CurСity;
            Cities.Add(SelectedCity);

            Warehouses = new ObservableCollection<WarehouseDTO>();
            SelectedWarehouse = Config.CurWarehouse;
            Warehouses.Add(SelectedWarehouse);

            tmpApiKeyNP = Config.ApiKeyNP;
            tmpLoginFly = Config.LoginFly;
            tmpPasswordFly = Config.PasswordFly;
            tmpCounterparty = Config.CurCounterparty;
            tmpContactPerson = Config.CurContactPerson;
            tmpCity = Config.CurСity;
            tmpWarehouse = Config.CurWarehouse;

            GetCounterpartiesCommand = new RelayCommand(GetCounterpartiesMethod);
            GetContactPersonsCommand = new RelayCommand(GetContactPersonsMethod);
            GetCitiesCommand = new RelayCommand(GetCitiesMethod);
            GetWarehousesCommand = new RelayCommand(GetWarehousesMethod);
            CancelCommand = new RelayCommand(CancelMethod);
            SaveSettingsCommand = new RelayCommand(SaveSettingsMethod);
            ChangeCounterpartyCommand = new RelayCommand(ChangeCounterpartyMethod);
            ChangeContactPersonCommand = new RelayCommand(ChangeContactPersonMethod);
            ChangeCityCommand = new RelayCommand(ChangeCityMethod);
            ChangeWarehouseCommand = new RelayCommand(ChangeWarehouseMethod);
        }

        private async void GetCounterpartiesMethod(object parameter)
        {
            Counterparties.Clear();
            Counterparties = await manager.GetCounterpartiesAsync();
        }

        private async void GetContactPersonsMethod(object parameter)
        {
            ContactPersons.Clear();
            ContactPersons = await manager.GetContactPersonsAsync();
        }

        private async void GetCitiesMethod(object parameter)
        {
            Cities.Clear();
            Cities = await manager.GetCitiesAsync();
        }

        private async void GetWarehousesMethod(object parameter)
        {
            Warehouses.Clear();
            Warehouses = await manager.GetWarehousesAsync(SelectedCity.Description);
        }        

        private void CancelMethod(object parameter)
        {
            Config.ApiKeyNP = tmpApiKeyNP;
            Config.LoginFly = tmpLoginFly;
            Config.PasswordFly = tmpPasswordFly;
            Config.CurCounterparty = tmpCounterparty;
            Config.CurContactPerson = tmpContactPerson;
            Config.CurСity = tmpCity;
            Config.CurWarehouse = tmpWarehouse;
            Application.Current.Windows[1].Close();            
        }

        private void SaveSettingsMethod(object parameter)
        {
            programConfigJSON.Save(Config, "programConfig.json");
            Application.Current.Windows[1].Close();
        }

        private void ChangeCounterpartyMethod(object parameter)
        {
            Config.CurCounterparty = SelectedCounterparty;
        }

        private void ChangeContactPersonMethod(object parameter)
        {
            Config.CurContactPerson = SelectedContactPerson;
        }

        private void ChangeCityMethod(object parameter)
        {
            Config.CurСity = SelectedCity;
            //Warehouses = await manager.GetWarehousesAsync(SelectedCity.Description);
        }

        private void ChangeWarehouseMethod(object parameter)
        {
            try
            {
                Config.CurWarehouse = SelectedWarehouse;
            }
            catch (Exception ex) { }
        }

    }
}
