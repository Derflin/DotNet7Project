using System.Collections.Generic;
using applicationApi.Models;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("api/sensors/wind")]
    public class WindSensorController : Controller
    {
        private readonly ILogger<WindSensorController> _logger;
        private readonly WindSensorService _windSensorService;

        public WindSensorController(ILogger<WindSensorController> logger, WindSensorService windSensorService)
        {
            this._logger = logger;
            this._windSensorService = windSensorService;
        }
        
        [HttpGet]
        public ActionResult<List<WindSensor>> Get() =>
            _windSensorService.Get();

        [HttpGet("{macAddress:length(12)}", Name = "GetWindSensor")]
        public ActionResult<List<WindSensor>> Get(string macAddress) => 
            _windSensorService.GetByMacAddress(macAddress);

        [HttpDelete("{macAddress:length(12)}")]
        public IActionResult Delete(string macAddress)
        {
            var windSensorList = _windSensorService.GetByMacAddress(macAddress);

            foreach (var windSensor in windSensorList)
            {
                _windSensorService.Remove(windSensor.Id);
            }

            return NoContent();
        }
    }
}