using MVVMNovaPoshta.BLL.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace MVVMNovaPoshta.BLL.Infrastructure
{
    public class NetworkManager
    {
        private ProgramConfig config;
        private ProgramConfigJSON programConfigJSON;

        public NetworkManager()
        {
            config = new ProgramConfig();
            programConfigJSON = new ProgramConfigJSON();
            config = programConfigJSON.Load("programConfig.json");
        }

        public string Tracking(string ttn)
        {
            string info = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"," +
                                    "\"modelName\": \"TrackingDocument\"," +
                                    "\"calledMethod\": \"getStatusDocuments\"," +
                                    "\"methodProperties\": {" +
                                                            "\"Documents\": [" +
                                                                                    "{" +
                                                                                        $"\"DocumentNumber\": \"{ttn}\"," +
                                                                                        "\"Phone\":\"\"" +
                                                                                    "}" +
                                                                           "]" +
                                                          "}" +
                                    "}";
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();
                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();
                    foreach (JToken result in results)
                    {
                        string number = result["Number"].ToString();
                        string status = result["Status"].ToString();
                        string сitySender = result["CitySender"].ToString();
                        string cityRecipient = result["CityRecipient"].ToString();
                        string scheduledDeliveryDate = result["ScheduledDeliveryDate"].ToString();
                        string documentWeight = result["DocumentWeight"].ToString();
                        string warehouseRecipient = result["WarehouseRecipient"].ToString();
                        info = $"Номер ТТН: {number}\n" +
                               $"Статус: {status}\n" +
                               $"Орієнтовна дата прибуття: {scheduledDeliveryDate}\n" +
                               $"Маршрут: {сitySender} - {cityRecipient}\n" +
                               $"Вага: {documentWeight} кг\n" +
                               $"Адреса доставки: {warehouseRecipient}\n";
                    }
                }
            }
            catch(Exception ex) { }
            
            return info;
        }

        public Task<string> TrackingAsync(string ttn)
        {
            return Task.Factory.StartNew(() => Tracking(ttn));
        }

        public ObservableCollection<CityDTO> GetCities()
        {
            ObservableCollection<CityDTO> cities = new ObservableCollection<CityDTO>();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"modelName\": \"Address\"," +
                                    "\"calledMethod\": \"getCities\"," +
                                    "\"methodProperties\": {}," +
                                    "\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"" +
                                    "}";
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();
                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        CityDTO city = new CityDTO();

                        string description = result["Description"].ToString();
                        city.Description = description;

                        string refCity = result["Ref"].ToString();
                        city.RefCity = refCity;

                        string area = result["Area"].ToString();
                        city.Area = area;

                        int cityID = Convert.ToInt32(result["CityID"].ToString());
                        city.CityID = cityID;

                        cities.Add(city);
                    }

                }
            }
            catch (Exception ex) { }
            
            return cities;
        }

        public Task<ObservableCollection<CityDTO>> GetCitiesAsync()
        {
            return Task.Factory.StartNew(() => GetCities());
        }

        public CityDTO GetCityByName(string cityName)
        {
            CityDTO city = new CityDTO();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"modelName\": \"Address\"," +
                                    "\"calledMethod\": \"getCities\"," +
                                    "\"methodProperties\": {" +
                                                                "\"FindByString\": \"" + $"{cityName}" + "\"" +
                                                          "}," +
                                    "\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"" +
                                    "}";
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();
                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        string description = result["Description"].ToString();
                        city.Description = description;

                        string refCity = result["Ref"].ToString();
                        city.RefCity = refCity;

                        string area = result["Area"].ToString();
                        city.Area = area;

                        int cityID = Convert.ToInt32(result["CityID"].ToString());
                        city.CityID = cityID;
                    }

                }
            }
            catch (Exception ex) { }

            return city;
        }

        public Task<CityDTO> GetCityByNameAsync(string cityName)
        {
            return Task.Factory.StartNew(() => GetCityByName(cityName));
        }

        public ObservableCollection<WarehouseDTO> GetWarehouses(string CityName)
        {
            ObservableCollection<WarehouseDTO> warehouses = new ObservableCollection<WarehouseDTO>();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"modelName\": \"AddressGeneral\"," +
                                    "\"calledMethod\": \"getWarehouses\"," +
                                    "\"methodProperties\": {" +
                                                            $"\"CityName\":\"{CityName}\"" +
                                                          "}," +
                                    "\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"" +
                                  "}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();
                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        WarehouseDTO warehouse = new WarehouseDTO();

                        string description = result["Description"].ToString();
                        warehouse.Description = description;

                        string warehouseRef = result["Ref"].ToString();
                        warehouse.WarehouseRef = warehouseRef;

                        int number = Convert.ToInt32(result["Number"].ToString());
                        warehouse.Number = number;

                        string cityRef = result["CityRef"].ToString();
                        warehouse.CityRef = cityRef;

                        string cityDescription = result["CityDescription"].ToString();
                        warehouse.CityDescription = cityDescription;
                    
                        warehouses.Add(warehouse);
                    }

                }
            }
            catch (Exception ex) { }
            return warehouses;
        }

        public Task<ObservableCollection<WarehouseDTO>> GetWarehousesAsync(string city)
        {
            return Task.Factory.StartNew(() => GetWarehouses(city));
        }

        public string SendSmsFly(DeliveryDTO SelDelivery)
        {
            string info = "";
            try
            {                
                string username = $"{config.LoginFly}";
                string password = $"{config.PasswordFly}";
                string messageText = $"{SelDelivery.DeliveryDescription} TTH: {SelDelivery.Ttn} Дата прибуття: {SelDelivery.DateArrival.Value.ToShortDateString()}";
                string url = "http://sms-fly.com/api/api.php";
                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
                
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                httpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
                httpWebRequest.ContentType = "text/xml";
                httpWebRequest.Accept = "text/xml";
                httpWebRequest.Method = "POST";

                var xmlRequest = new XElement("request",
                                                new XElement("operation", "SENDSMS"),
                                                new XElement("message",
                                                    new XAttribute("start_time", "AUTO"),
                                                    new XAttribute("end_time", "AUTO"),
                                                    new XAttribute("lifetime", "4"),
                                                    new XAttribute("rate", "120"),
                                                    new XAttribute("desc", "SMS sending"),
                                                    new XAttribute("source", "InfoCentr"),
                                                    new XElement("body", $"{messageText}"),
                                                    new XElement("recipient", $"{SelDelivery.CustomerPhone}")));

                byte[] bytes = Encoding.UTF8.GetBytes(xmlRequest.ToString());
                httpWebRequest.ContentLength = bytes.Length;

                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    var responseXml = XDocument.Load(httpResponse.GetResponseStream());
                    foreach (XElement element in responseXml.Elements("message"))
                    {
                        if ((string)element.Element("state").Attribute("code") == "ACCEPT")
                        {
                            info = $"Номер телефону: {(string)element.Element("to").Attribute("recipient")}\n" +
                                   $"Статус: {(string)element.Element("to").Attribute("status")}";
                        }
                        else
                        {
                            info = "Error";
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return info;
        }

        public Task<string> SendSmsFlyAsync(DeliveryDTO SelDelivery)
        {
            return Task.Factory.StartNew(() => SendSmsFly(SelDelivery));
        }

        public ObservableCollection<Counterparty> GetCounterparties()
        {            
            ObservableCollection<Counterparty> counterparties = new ObservableCollection<Counterparty>();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {                   
                    string json = "{\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"," +
                                        "\"modelName\": \"Counterparty\"," +
                                        "\"calledMethod\": \"getCounterparties\"," +
                                        "\"methodProperties\": {" +
                                                            "\"CounterpartyProperty\": \"Sender\"," +
                                                            "\"Page\": \"1\"" +
                                                            "}" +
                                    "}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();
                    
                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();
                    foreach (JToken result in results)
                    {
                        Counterparty counterparty = new Counterparty();

                        string description = result["Description"].ToString();
                        counterparty.Description = description;

                        string refCounterparty = result["Ref"].ToString();
                        counterparty.RefCounterparty = refCounterparty;

                        string city = result["City"].ToString();
                        counterparty.City = city;

                        counterparties.Add(counterparty);
                    }
                }
            }
            catch (Exception ex) { }

            return counterparties;
        }

        public Task<ObservableCollection<Counterparty>> GetCounterpartiesAsync()
        {
            return Task.Factory.StartNew(() => GetCounterparties());
        }

        public ObservableCollection<CounterpartyContactPerson> GetContactPersons()
        {
            ObservableCollection<CounterpartyContactPerson> contactPersons = new ObservableCollection<CounterpartyContactPerson>();

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"," +
                                        "\"modelName\": \"Counterparty\"," +
                                        "\"calledMethod\": \"getCounterpartyContactPersons\"," +
                                        "\"methodProperties\": {" +
                                                            "\"Ref\": \"" + $"{config.CurCounterparty.RefCounterparty}" + "\"," +
                                                            "\"Page\": \"1\"" +
                                                            "}" +
                                    "}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();

                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();
                    foreach (JToken result in results)
                    {
                        CounterpartyContactPerson contactPerson = new CounterpartyContactPerson();

                        string description = result["Description"].ToString();
                        contactPerson.Description = description;

                        string refContactPerson = result["Ref"].ToString();
                        contactPerson.RefContactPerson = refContactPerson;

                        string phones = result["Phones"].ToString();
                        contactPerson.Phones = phones;

                        contactPersons.Add(contactPerson);
                    }
                }
            }
            catch (Exception ex) { }

            return contactPersons;
        }

        public Task<ObservableCollection<CounterpartyContactPerson>> GetContactPersonsAsync()
        {
            return Task.Factory.StartNew(() => GetContactPersons());
        }

        public Tuple<string, string> CreateCounterparty(DeliveryDTO SelDelivery)
        {
            Tuple<string, string> tuple = new Tuple<string, string>("", "");
            string[] fullName = SelDelivery.CustomerName.Split(' ');

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"," +
                                        "\"modelName\": \"Counterparty\"," +
                                        "\"calledMethod\": \"save\"," +
                                        "\"methodProperties\": {" +
                                                                    "\"FirstName\": \"" + $"{fullName[1]}" + "\"," +
                                                                    "\"MiddleName\": \"" + $"{fullName[2]}" + "\"," +
                                                                    "\"LastName\": \"" + $"{fullName[0]}" + "\"," +
                                                                    "\"Phone\": \"" + $"{SelDelivery.CustomerPhone}" + "\"," +
                                                                    "\"Email\": \"\"," +
                                                                    "\"CounterpartyType\": \"PrivatePerson\"," +
                                                                    "\"CounterpartyProperty\": \"Recipient\"" +
                                                              "}" +
                                    "}";

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();                    
                    JObject search = JObject.Parse(str);
                    string refCounterparty = (string)search["data"][0]["Ref"];
                    string refContactPerson = (string)search["data"][0]["ContactPerson"]["data"][0]["Ref"];
                    tuple = new Tuple<string,string>(refCounterparty, refContactPerson);
                }
            }
            catch (Exception ex) { }
            return tuple;
        }

        public Task<Tuple<string, string>> CreateCounterpartyAsync(DeliveryDTO SelDelivery)
        {
            return Task.Factory.StartNew(() => CreateCounterparty(SelDelivery));
        }

        public TtnInfo CreateTtn(DeliveryDTO SelectedDelivery)        
        {
            Tuple<string, string> recipCounterparty = CreateCounterparty(SelectedDelivery);
            string RecipientRef = recipCounterparty.Item1;
            string ContactRecipientRef = recipCounterparty.Item2;
            string CityRecipientRef = (GetCityByName(SelectedDelivery.City)).RefCity;
            string RecipientAddressRef = (GetWarehouses(SelectedDelivery.City)).FirstOrDefault(x => x.Number == SelectedDelivery.NumStorage).WarehouseRef;
            
            TtnInfo ttn = new TtnInfo();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.novaposhta.ua/v2.0/json/");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"apiKey\": \"" + $"{config.ApiKeyNP}" + "\"," +
                                    "\"modelName\": \"InternetDocument\"," +
                                    "\"calledMethod\": \"save\"," +
                                    "\"methodProperties\": {" +
                                                            "\"PayerType\": \"Sender\"," +
                                                            "\"PaymentMethod\": \"Cash\"," +
                                                            "\"DateTime\": \"" + $"{DateTime.Now.ToShortDateString()}" + "\"," +
                                                            "\"CargoType\": \"Cargo\"," +
                                                            "\"VolumeGeneral\": \"" + $"{SelectedDelivery.WeightMax/250.0}" + "\"," +
                                                            "\"Weight\": \"" + $"{SelectedDelivery.WeightMax}" + "\"," +
                                                            "\"ServiceType\": \"WarehouseDoors\"," +
                                                            "\"SeatsAmount\": \"1\"," +
                                                            "\"Description\": \"" + $"{SelectedDelivery.DeliveryDescription}" + "\"," +
                                                            "\"Cost\": \"" + $"{SelectedDelivery.Price}" + "\"," +
                                                            "\"CitySender\": \"" + $"{config.CurСity.RefCity}" + "\"," +
                                                            "\"Sender\": \"" + $"{config.CurCounterparty.RefCounterparty}" + "\"," +
                                                            "\"SenderAddress\": \"" + $"{config.CurWarehouse.WarehouseRef}" + "\"," +
                                                            "\"ContactSender\": \"" + $"{config.CurContactPerson.RefContactPerson}" + "\"," +
                                                            "\"SendersPhone\": \"" + $"{config.CurContactPerson.Phones}" + "\"," +
                                                            "\"CityRecipient\": \"" + $"{CityRecipientRef}" + "\"," +
                                                            "\"Recipient\": \"" + $"{RecipientRef}" + "\"," +
                                                            "\"RecipientAddress\": \"" + $"{RecipientAddressRef}" + "\"," +
                                                            "\"ContactRecipient\": \"" + $"{ContactRecipientRef}" + "\"," +
                                                            "\"RecipientsPhone\": \"" + $"{SelectedDelivery.CustomerPhone}" + "\"" +
                                                        "}" +
                                    "}";
                    
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var str = streamReader.ReadToEnd();
                    
                    JObject search = JObject.Parse(str);
                    IList<JToken> results = search["data"].Children().ToList();

                    foreach (JToken result in results)
                    {
                        string ttnRef = result["Ref"].ToString();
                        ttn.TtnRef = ttnRef;

                        int costOnSite = (int)result["CostOnSite"];
                        ttn.CostOnSite = costOnSite;

                        DateTime estimatedDeliveryDate = (DateTime)result["EstimatedDeliveryDate"];
                        ttn.EstimatedDeliveryDate = estimatedDeliveryDate;

                        string intDocNumber = result["IntDocNumber"].ToString();
                        ttn.IntDocNumber = intDocNumber;

                        string typeDocument = result["TypeDocument"].ToString();
                        ttn.TypeDocument = typeDocument;
                    }
                }
            }
            catch (Exception ex) { }
            return ttn;
        }

        public Task<TtnInfo> CreateTtnAsync(DeliveryDTO SelectedDelivery)
        {
            return Task.Factory.StartNew(() => CreateTtn(SelectedDelivery));             
        }

        //https://my.novaposhta.ua/orders/printMarkings/orders/20450243027176/type/html/apiKey/5a14a4d8a0ae4e8b5b9bb730cbdb44fd  //печать маркировок

    }
}
