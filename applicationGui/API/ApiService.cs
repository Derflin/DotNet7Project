using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using applicationGui.Models;
using Newtonsoft.Json;

namespace applicationGui.API
{
    //klasa wywołuje odpowiednie endpointy serwera 
    public class ApiService
    {
        public PaginatedListSensor<PressureSensor> GetPressureSensorData(string address = null, string minDate = null,
            string maxDate = null, int page = 0, int size = 0, string sort = null, string order = null)
        {
            string responseBody = null;

            if (address != null || minDate != null || maxDate != null || page > 0 || size > 0 || sort != null || order != null)
            {
                responseBody = _makeHttpGet($"http://actina15.maas:17584/api/sensors/pressure{CreateQuery(address,minDate,maxDate,page,size,sort,order)}");
            }
            else
            {
                responseBody = _makeHttpGet("http://actina15.maas:17584/api/sensors/pressure");
            }

            //List<PressureSensor> parsedList = JsonConvert.DeserializeObject<List<PressureSensor>>(responseBody);
            PaginatedListSensor<PressureSensor> parsedList = JsonConvert.DeserializeObject<PaginatedListSensor<PressureSensor>>(responseBody);

            /*
            List<PressureSensor> parsedList = new List<PressureSensor>
            {
                new PressureSensor(id: "1", macAddress: "1", dateTime: DateTime.Now, type: "1", pressure: 100),
                new PressureSensor(id: "2", macAddress: "1", dateTime: DateTime.Now.AddHours(1), type: "1",
                    pressure: 110),
                new PressureSensor(id: "3", macAddress: "1", dateTime: DateTime.Now.AddHours(2), type: "1",
                    pressure: 140),
            };
            */
            
            return parsedList;
        }

        public PaginatedListSensor<HumiditySensor> GetHumiditySensorData(string address = null, string minDate = null,
            string maxDate = null, int page = 0, int size = 0, string sort = null, string order = null)
        {
            string responseBody = null;

            if (address != null || minDate != null || maxDate != null || page > 0 || size > 0 || sort != null || order != null)
            {
                responseBody = _makeHttpGet($"http://actina15.maas:17584/api/sensors/humidity{CreateQuery(address,minDate,maxDate,page,size,sort,order)}");
            }
            else
            {
                responseBody = _makeHttpGet("http://actina15.maas:17584/api/sensors/humidity");
            }
            
            //List<HumiditySensor> parsedList = JsonConvert.DeserializeObject<List<HumiditySensor>>(responseBody);
            PaginatedListSensor<HumiditySensor> parsedList = JsonConvert.DeserializeObject<PaginatedListSensor<HumiditySensor>>(responseBody);
            /*
            List<HumiditySensor> parsedList = new List<HumiditySensor>
            {
                new HumiditySensor(id: "1", macAddress: "1", dateTime: DateTime.Now, type: "1", humidity: 100),
                new HumiditySensor(id: "2", macAddress: "1", dateTime: DateTime.Now.AddHours(1), type: "1",
                    humidity: 45),
                new HumiditySensor(id: "3", macAddress: "1", dateTime: DateTime.Now.AddHours(2), type: "1",
                    humidity: 85),
            };
            */

            return parsedList;
        }

        public PaginatedListSensor<TemperatureSensor> GetTemperatureSensorData(string address = null, string minDate = null,
            string maxDate = null, int page = 0, int size = 0, string sort = null, string order = null)
        {
            string responseBody = null;

            if (address != null || minDate != null || maxDate != null || page > 0 || size > 0 || sort != null || order != null)
            {
                responseBody = _makeHttpGet($"http://actina15.maas:17584/api/sensors/temperature{CreateQuery(address,minDate,maxDate,page,size,sort,order)}");
            }
            else
            {
                responseBody = _makeHttpGet("http://actina15.maas:17584/api/sensors/temperature");
            }

            PaginatedListSensor<TemperatureSensor> parsedList = JsonConvert.DeserializeObject<PaginatedListSensor<TemperatureSensor>>(responseBody);

            /*
            List<TemperatureSensor> parsedList = new List<TemperatureSensor>();
            Random rnd = new Random();
            for (uint ctr = 1; ctr <= 50; ctr++)
            {
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius:  rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
            }
            //*/
            
            return parsedList;
        }

        public PaginatedListSensor<WindSensor> GetWindSensorData(string address = null, string minDate = null,
            string maxDate = null, int page = 0, int size = 0, string sort = null, string order = null)
        {
            string responseBody = null;

            if (address != null || minDate != null || maxDate != null || page > 0 || size > 0 || sort != null || order != null)
            {
                responseBody = _makeHttpGet($"http://actina15.maas:17584/api/sensors/wind{CreateQuery(address,minDate,maxDate,page,size,sort,order)}");
            }
            else
            {
                responseBody = _makeHttpGet("http://actina15.maas:17584/api/sensors/wind");
            }

            PaginatedListSensor<WindSensor> parsedList = JsonConvert.DeserializeObject<PaginatedListSensor<WindSensor>>(responseBody);

            /*
            List<WindSensor> parsedList = new List<WindSensor>
            {
                new WindSensor(id: "1", macAddress: "1", dateTime: DateTime.Now, type: "1", speed: 20, direction: 100),
                new WindSensor(id: "2", macAddress: "1", dateTime: DateTime.Now.AddHours(1), type: "1", speed: 25,
                    direction: 100),
                new WindSensor(id: "3", macAddress: "1", dateTime: DateTime.Now.AddHours(2), type: "1", speed: 15,
                    direction: 100),
            };
            */

            return parsedList;
        }

        public List<string> GetAllSensors()
        {
            var responseBody = _makeHttpGet("http://actina15.maas:17584/api/sensors");
            List<string> parsedList = JsonConvert.DeserializeObject<List<string>>(responseBody);
            
            return parsedList;
        }

        private string CreateQuery(string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            StringBuilder queryBody = new StringBuilder();
            
            queryBody.Append('?');

            if (address != null)
                queryBody.Append($"address={address.Replace(":", "%3A")}&");
            if (minDate != null)
                queryBody.Append($"minDate={minDate}&");
            if (maxDate != null)
                queryBody.Append($"maxDate={maxDate}&");
            if (page > 0)
                queryBody.Append($"page={page}&");
            if (size > 0)
                queryBody.Append($"size={size}&");
            if (sort != null)
                queryBody.Append($"sort={sort}&");
            if (order != null)
                queryBody.Append($"order={order}&");
                
            queryBody.Remove(queryBody.Length-1, 1);

            return queryBody.ToString();
        }
        
        public string _makeHttpGet(string url)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                    true;

                HttpClient client = new HttpClient(clientHandler);
                Task<HttpResponseMessage> futureResponse = client.GetAsync(url);
                futureResponse.Wait();
                HttpResponseMessage response = futureResponse.Result;

                response.EnsureSuccessStatusCode();

                Task<string> futureString = response.Content.ReadAsStringAsync();
                futureString.Wait();
                return futureString.Result;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return null;
        }
    }
}