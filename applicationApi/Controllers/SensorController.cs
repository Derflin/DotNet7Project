using System;
using System.Collections.Generic;
using System.Linq;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorController : Controller
    {
        private readonly ILogger<SensorController> _logger;
        private readonly HumiditySensorService _humiditySensorService;
        private readonly PressureSensorService _pressureSensorService;
        private readonly TemperatureSensorService _temperatureSensorService;
        private readonly WindSensorService _windSensorService;

        public SensorController(ILogger<SensorController> logger, 
            HumiditySensorService humiditySensorService, 
            PressureSensorService pressureSensorService, 
            TemperatureSensorService temperatureSensorService,
            WindSensorService windSensorService)
        {
            this._logger = logger;
            this._humiditySensorService = humiditySensorService;
            this._pressureSensorService = pressureSensorService;
            this._temperatureSensorService = temperatureSensorService;
            this._windSensorService = windSensorService;
        }
        
        [HttpGet]
        public List<String> GetDistinctSensors()
        {
            var listHumidity = _humiditySensorService.GetDistinctMacAddresses();
            var listPressure = _pressureSensorService.GetDistinctMacAddresses();
            var listTemperature = _temperatureSensorService.GetDistinctMacAddresses();
            var listWind = _windSensorService.GetDistinctMacAddresses();
            var outputList = listHumidity.Union(listPressure).Union(listTemperature).Union(listWind).ToList();
            return outputList;
        }
    }
}