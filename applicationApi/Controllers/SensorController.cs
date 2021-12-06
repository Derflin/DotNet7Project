using System;
using System.Collections.Generic;
using System.Linq;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("api/sensors")]
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
        public List<string> GetDistinctSensors()
        {
            _logger.LogInformation("Get request acquired for list of all sensors addresses");
            var listHumidity = _humiditySensorService.GetDistinctMacAddresses();
            var listPressure = _pressureSensorService.GetDistinctMacAddresses();
            var listTemperature = _temperatureSensorService.GetDistinctMacAddresses();
            var listWind = _windSensorService.GetDistinctMacAddresses();
            var outputList = listHumidity.Union(listPressure).Union(listTemperature).Union(listWind).ToList();
            return outputList;
        }

        [HttpDelete]
        public IActionResult ClearData()
        {
            _logger.LogInformation("Delete request acquired for clearing database data");
            _humiditySensorService.RemoveAll();
            _pressureSensorService.RemoveAll();
            _temperatureSensorService.RemoveAll();
            _windSensorService.RemoveAll();
            
            return NoContent();
        }
    }
}