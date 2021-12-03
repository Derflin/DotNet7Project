using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using applicationGui.Models;
using Newtonsoft.Json;

namespace applicationGui.API
{
    //klasa wywołuje odpowiednie endpointy serwera 
    public class ApiService
    {
        public List<PressureSensor> GetPressureSensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:17584/api/sensors/pressure");
            List<PressureSensor> parsedList = JsonConvert.DeserializeObject<List<PressureSensor>>(responseBody);

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

        public List<HumiditySensor> GetHumiditySensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:17584/api/sensors/humidity");
            List<HumiditySensor> parsedList = JsonConvert.DeserializeObject<List<HumiditySensor>>(responseBody);

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

        public List<TemperatureSensor> GetTemperatureSensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:17584/api/sensors/temperature");
            List<TemperatureSensor> parsedList = JsonConvert.DeserializeObject<List<TemperatureSensor>>(responseBody);

            /*
            List<TemperatureSensor> parsedList = new List<TemperatureSensor>();
            Random rnd = new Random();
            for (uint ctr = 1; ctr <= 50; ctr++)
            {
                parsedList.Add(new TemperatureSensor(id: "1", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius:  rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "2", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "3", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "4", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "5", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "6", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "7", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "8", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "9", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "10", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
                parsedList.Add(new TemperatureSensor(id: "11", macAddress: "1", dateTime: DateTime.Now.AddHours(ctr), type: "1",
                    celsius: rnd.Next(250), fahrenheit: rnd.Next(250)));
            }
            */
            
            return parsedList;
        }

        public List<WindSensor> GetWindSensorData()
        {
            var responseBody = _makeHttpGet("http://localhost:17584/api/sensors/wind");
            List<WindSensor> parsedList = JsonConvert.DeserializeObject<List<WindSensor>>(responseBody);

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