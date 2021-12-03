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
            _humiditySensorService.RemoveAll();
            _pressureSensorService.RemoveAll();
            _temperatureSensorService.RemoveAll();
            _windSensorService.RemoveAll();
            
            return NoContent();
        }
        
        /*
         TODO: something with formatters
        [HttpGet("getDistinctSensorsJson")]
        public IActionResult ListJson()
        {
            List<String> sensors = this.GetDistinctSensors();

            return Ok(sensors);
        }
        
        [Produces("text/csv")]
        [HttpGet("getDistinctSensorsCsv")]
        public IActionResult ListCsv()
        {
            List<String> sensors = this.GetDistinctSensors();

            return Ok(sensors);
        }
        */
    }
}