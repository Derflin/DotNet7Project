using System;
using System.Collections.Generic;
using System.Linq;
using applicationGui.API;
using applicationGui.Models;
using Microsoft.AspNetCore.Mvc;

namespace applicationGui.Controllers
{
    // kontroleń żadań REST
    [Route("Rest")]
    public class RestController : ControllerBase
    {
        private ApiService _apiService;

        public RestController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // zwykła przeplotka wywołujaca metodę serwisu
        [FormatFilter]
        [HttpGet("{format}/pressure")]
        public List<PressureSensor> getPressureData(string format) => _apiService.GetPressureSensorData().Items;

        // metoda pobierająca dane z serwisu 
        // nastepnie kwantyzuje z doksładnościa do sekundy
        // następnie grupuje dane na podstawie ID sensora
        [HttpGet("{format}/pressure/avg")]
        public List<PressureSensor> getPressureDataAvg()
        {
            List<PressureSensor> avg = _apiService.GetPressureSensorData().Items;

            if (avg == null) return new List<PressureSensor>();

            List<PressureSensor> rawSensorsData = new List<PressureSensor>();
            foreach (var sensorData in avg)
            {
                var tmpData = new PressureSensor()
                {
                    DateTime = RoundUp(sensorData.DateTime, TimeSpan.FromSeconds(1)),
                    Pressure = sensorData.Pressure,
                };

                rawSensorsData.Add(tmpData);
            }

            List<IGrouping<DateTime, PressureSensor>> groupedSensors = rawSensorsData.GroupBy(e => e.DateTime).ToList();

            List<PressureSensor> flattenList = new List<PressureSensor>();
            foreach (IGrouping<DateTime, PressureSensor> sensorData in groupedSensors)
            {
                var tmpData = new PressureSensor()
                {
                    DateTime = sensorData.Key,
                    Pressure = Convert.ToInt32(sensorData.Average(e => e.Pressure)),
                };

                flattenList.Add(tmpData);
            }
            return flattenList;
        }


        [FormatFilter]
        [HttpGet("{format}/humidity")]
        public List<HumiditySensor> getHumidityData(string format) => _apiService.GetHumiditySensorData().Items;


        [HttpGet("{format}/humidity/avg")]
        public List<HumiditySensor> getHumidityDataAvg()
        {
            List<HumiditySensor> avg = _apiService.GetHumiditySensorData().Items;

            if (avg == null) return new List<HumiditySensor>();

            List<HumiditySensor> rawSensorsData = new List<HumiditySensor>();
            foreach (var sensorData in avg)
            {
                var tmpData = new HumiditySensor()
                {
                    DateTime = RoundUp(sensorData.DateTime, TimeSpan.FromSeconds(1)),
                    Humidity = sensorData.Humidity,
                };

                rawSensorsData.Add(tmpData);
            }

            List<IGrouping<DateTime, HumiditySensor>> groupedSensors = rawSensorsData.GroupBy(e => e.DateTime).ToList();

            List<HumiditySensor> flattenList = new List<HumiditySensor>();
            foreach (IGrouping<DateTime, HumiditySensor> sensorData in groupedSensors)
            {
                var tmpData = new HumiditySensor()
                {
                    DateTime = sensorData.Key,
                    Humidity = Convert.ToInt32(sensorData.Average(e => e.Humidity)),
                };

                flattenList.Add(tmpData);
            }
            return flattenList;
        }

        [FormatFilter]
        [HttpGet("{format}/temperature")]
        public List<TemperatureSensor> getTemperatureData(string format) => _apiService.GetTemperatureSensorData().Items;
        
        [HttpGet("{format}/temperature/avg")]
        public List<TemperatureSensor> getTemperatureDataAvg()
        {
            List<TemperatureSensor> avg = _apiService.GetTemperatureSensorData().Items;

            if (avg == null) return new List<TemperatureSensor>();

            List<TemperatureSensor> rawSensorsData = new List<TemperatureSensor>();
            foreach (var sensorData in avg)
            {
                var tmpData = new TemperatureSensor()
                {
                    DateTime = RoundUp(sensorData.DateTime, TimeSpan.FromSeconds(1)),
                    Celsius = sensorData.Celsius,
                    Fahrenheit = sensorData.Fahrenheit,
                };

                rawSensorsData.Add(tmpData);
            }

            List<IGrouping<DateTime, TemperatureSensor>> groupedSensors =
                rawSensorsData.GroupBy(e => e.DateTime).ToList();

            List<TemperatureSensor> flattenList = new List<TemperatureSensor>();
            foreach (IGrouping<DateTime, TemperatureSensor> sensorData in groupedSensors)
            {
                var tmpData = new TemperatureSensor()
                {
                    DateTime = sensorData.Key,
                    Celsius = Convert.ToInt32(sensorData.Average(e => e.Celsius)),
                    Fahrenheit = Convert.ToInt32(sensorData.Average(e => e.Fahrenheit)),
                };

                flattenList.Add(tmpData);
            }
            return flattenList;
        }

        [FormatFilter]
        [HttpGet("{format}/wind")]
        public List<WindSensor> getWindData(string format) => _apiService.GetWindSensorData().Items;


        [HttpGet("{format}/wind/avg")]
        public List<WindSensor> getWindSpeedDataAvg()
        {
            List<WindSensor> avg = _apiService.GetWindSensorData().Items;

            if (avg == null) return new List<WindSensor>();

            List<WindSensor> rawSensorsData = new List<WindSensor>();
            foreach (var sensorData in avg)
            {
                var tmpData = new WindSensor()
                {
                    DateTime = RoundUp(sensorData.DateTime, TimeSpan.FromSeconds(1)),
                    Speed = sensorData.Speed,
                };

                rawSensorsData.Add(tmpData);
            }

            List<IGrouping<DateTime, WindSensor>> groupedSensors = rawSensorsData.GroupBy(e => e.DateTime).ToList();

            List<WindSensor> flattenList = new List<WindSensor>();
            foreach (IGrouping<DateTime, WindSensor> sensorData in groupedSensors)
            {
                var tmpData = new WindSensor()
                {
                    DateTime = sensorData.Key,
                    Speed = Convert.ToInt32(sensorData.Average(e => e.Speed)),
                };

                flattenList.Add(tmpData);
            }
            return flattenList;
        }
        
        [HttpGet("{format}")]
        public List<string> GetSensorsMac(string format) => _apiService.GetAllSensors();

        public static DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            var modTicks = dt.Ticks % d.Ticks;
            var delta = modTicks != 0 ? d.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }
    }
}