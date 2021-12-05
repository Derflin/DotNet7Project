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
        public IActionResult TemperatureTable([FromQuery] string sort, [FromQuery] string order)
        {
            ViewData.Add("NAME", "Temperature");
            ViewData["SORT"] = sort;
            ViewData["ORDER"] = order;

            // pobranie danych
            List<TemperatureSensor> sensor = _apiService.GetTemperatureSensorData(sort: sort, order: order).Items;
            
            // grupowanie danych na podstwie ID sensora
            List<List<TemperatureSensor>> splitedSensors = _splitSensors(sensor);

            return View("TemperatureTable", splitedSensors);
        }

        public IActionResult PressureTable([FromQuery] string sort, [FromQuery] string order)
        {
            ViewData.Add("NAME", "Pressure");
            ViewData["SORT"] = sort;
            ViewData["ORDER"] = order;

            var sensor = _apiService.GetPressureSensorData(sort: sort, order: order).Items;
            List<List<PressureSensor>> splitedSensors = _splitSensors(sensor);

            return View("PressureTable", splitedSensors);
        }

        public IActionResult HumidityTable([FromQuery] string sort, [FromQuery] string order)
        {
            ViewData.Add("NAME", "Humidity");
            ViewData["SORT"] = sort;
            ViewData["ORDER"] = order;

            var sensor = _apiService.GetHumiditySensorData(sort: sort, order: order).Items;
            List<List<HumiditySensor>> splitedSensors = _splitSensors(sensor);

            return View("HumidityTable", splitedSensors);
        }

        public IActionResult WindTable([FromQuery] string sort, [FromQuery] string order)
        {
            ViewData.Add("NAME", "Wind");
            ViewData["SORT"] = sort;
            ViewData["ORDER"] = order;

            var sensor = _apiService.GetWindSensorData(sort: sort, order: order).Items;
            List<List<WindSensor>> splitedSensors = _splitSensors(sensor);

            return View("WindTable", splitedSensors);
        }

        public IActionResult SensorTable([FromQuery] string mac, [FromQuery] string minDate, [FromQuery] string maxDate,
            [FromQuery] int page, [FromQuery] int size, [FromQuery] string sort, [FromQuery] string order)
        {
            ViewData["MINDATE"] = minDate;
            ViewData["MAXDATE"] = maxDate;
            ViewData["SORT"] = sort;
            ViewData["ORDER"] = order;
            
            var sensorWind = _apiService.GetWindSensorData(mac, minDate, maxDate, page, size, sort, order);

            if (sensorWind.TotalItems > 0)
            {
                if (sensorWind.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Wind Sensor Data - {mac}");
                    ViewData.Add("MAC", mac);
                
                    return View("WindSensorView", sensorWind);
                }
                return Redirect("/");
            }
            
            var sensorHumidity = _apiService.GetHumiditySensorData(mac, minDate, maxDate, page, size, sort, order);

            if (sensorHumidity.TotalItems != 0)
            {
                if (sensorHumidity.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Humidity Sensor Data - {mac}");
                    ViewData.Add("MAC", mac);
                
                    return View("HumiditySensorView", sensorHumidity);
                }

                return Redirect("/");
            }
            var sensorPressure = _apiService.GetPressureSensorData(mac, minDate, maxDate, page, size, sort, order);

            if (sensorPressure.TotalItems > 0)
            {
                if (sensorPressure.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Pressure Sensor Data - {mac}");
                    ViewData.Add("MAC", mac);
                
                    return View("PressureSensorView", sensorPressure);
                }
                return Redirect("/");
            }
            var sensorTemperature = _apiService.GetTemperatureSensorData(mac, minDate, maxDate, page, size, sort, order);

            if (sensorTemperature.TotalItems > 0)
            {
                if (sensorTemperature.Items.Count > 0)
                {
                    ViewData.Add("NAME", $"Temperature Sensor Data - {mac}");
                    ViewData.Add("MAC", mac);
                
                    return View("TemperatureSensorView", sensorTemperature);
                }
                return Redirect("/");
            }
            
            return View("Error");
        }

        public IActionResult SortTable(string mac, string minDate, string maxDate, int page, int size, string sort, 
            string oldSort, string oldOrder, string actionName)
        {
            string order = null;

            oldSort ??= "date";
            oldOrder ??= "desc";

            if (!oldSort.Equals(sort))
            {
                order = "desc";
            }
            else
            {
                if (oldOrder.Equals("desc"))
                {
                    order = "asc";
                }
                else
                {
                    order = "desc";
                }
            }

            switch (actionName)
            {
                case "SensorTable":
                    return RedirectToAction("SensorTable", new {mac, minDate, maxDate, page, size, sort, order});
                case "WindTable":
                    return RedirectToAction("WindTable", new {sort, order});
                case "TemperatureTable":
                    return RedirectToAction("TemperatureTable", new {sort, order});
                case "PressureTable":
                    return RedirectToAction("PressureTable", new {sort, order});
                case "HumidityTable":
                    return RedirectToAction("HumidityTable", new {sort, order});
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult SetFilter(string mac, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            return RedirectToAction("SensorTable", new {mac, minDate, maxDate, page, size, sort, order});
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