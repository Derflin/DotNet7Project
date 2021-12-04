using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using applicationGui.Models;
using applicationGui.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using applicationGui.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace applicationGui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApiService _apiService;

        public HomeController(ILogger<HomeController> logger, ApiService apiService)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            List<string> sensors = _apiService.GetAllSensors();
            TempData.Add("MACLIST", sensors);

            return View();
        }

        public IActionResult Privacy() => View();

        public IActionResult License() => View();

        // strona z tablą z odczytami sensorów temperatury
        public IActionResult TemperatureTable()
        {
            ViewData.Add("NAME", "Temperature");

            // pobranie danych
            List<TemperatureSensor> sensor = _apiService.GetTemperatureSensorData().Items;
            
            // grupowanie danych na podstwie ID sensora
            List<List<TemperatureSensor>> splitedSensors = _splitSensors(sensor);

            return View("TemperatureTable", splitedSensors);
        }

        public IActionResult PressureTable()
        {
            ViewData.Add("NAME", "Pressure");

            var sensor = _apiService.GetPressureSensorData().Items;
            List<List<PressureSensor>> splitedSensors = _splitSensors(sensor);

            return View("PressureTable", splitedSensors);
        }

        public IActionResult HumidityTable()
        {
            ViewData.Add("NAME", "Humidity");

            var sensor = _apiService.GetHumiditySensorData().Items;
            List<List<HumiditySensor>> splitedSensors = _splitSensors(sensor);

            return View("HumidityTable", splitedSensors);
        }

        public IActionResult WindTable()
        {
            ViewData.Add("NAME", "Wind");

            var sensor = _apiService.GetWindSensorData().Items;
            List<List<WindSensor>> splitedSensors = _splitSensors(sensor);

            return View("WindTable", splitedSensors);
        }

        public IActionResult SensorTable([FromQuery] string mac, [FromQuery] int page, [FromQuery] int size)
        {
            var sensorWind = _apiService.GetWindSensorData(mac, page, size);

            if (sensorWind.TotalItems > 0)
            {
                if (sensorWind.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Wind Sensor Data - {mac}");
                
                    return View("WindSensorView", sensorWind);
                }
                return Redirect("/");
            }
            
            var sensorHumidity = _apiService.GetHumiditySensorData(mac, page, size);

            if (sensorHumidity.TotalItems != 0)
            {
                if (sensorHumidity.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Humidity Sensor Data - {mac}");
                
                    return View("HumiditySensorView", sensorHumidity);
                }

                return Redirect("/");
            }
            var sensorPressure = _apiService.GetPressureSensorData(mac, page, size);

            if (sensorPressure.TotalItems > 0)
            {
                if (sensorPressure.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Pressure Sensor Data - {mac}");
                
                    return View("PressureSensorView", sensorPressure);
                }
                return Redirect("/");
            }
            var sensorTemperature = _apiService.GetTemperatureSensorData(mac, page, size);

            if (sensorTemperature.TotalItems > 0)
            {
                if (sensorTemperature.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Temperature Sensor Data - {mac}");
                
                    return View("TemperatureSensorView", sensorTemperature);
                }
                return Redirect("/");
            }
            
            return View("Error");
        }

        // metoda grupuje odczyty na podstawie id sensora
        private List<List<T>> _splitSensors<T>(IReadOnlyCollection<T> sensor) where T : Sensor
        {
            if (sensor == null || sensor.FirstOrDefault() == null) return new List<List<T>>();

            List<IGrouping<string, T>> list = sensor.GroupBy(u => u.MacAddress).ToList();

            List<List<T>> splitedSensors = new List<List<T>>();

            foreach (IGrouping<string, T> sen in list)
            {
                splitedSensors.Add(sen.ToList());
            }

            return splitedSensors;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

    }
}