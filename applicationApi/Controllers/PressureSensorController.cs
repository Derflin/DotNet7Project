using System.Collections.Generic;
using applicationApi.Models;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("api/sensors/pressure")]
    public class PressureSensorController : Controller
    {
        private readonly ILogger<PressureSensorController> _logger;
        private readonly PressureSensorService _pressureSensorService;

        public PressureSensorController(ILogger<PressureSensorController> logger, PressureSensorService pressureSensorService)
        {
            this._logger = logger;
            this._pressureSensorService = pressureSensorService;
        }
        
        [HttpGet]
        public ActionResult<List<PressureSensor>> Get() =>
            _pressureSensorService.Get();

        [HttpGet("{macAddress:length(12)}", Name = "GetPressureSensor")]
        public ActionResult<List<PressureSensor>> Get(string macAddress) => 
            _pressureSensorService.GetByMacAddress(macAddress);

        [HttpDelete("{macAddress:length(12)}")]
        public IActionResult Delete(string macAddress)
        {
            var pressureSensorList = _pressureSensorService.GetByMacAddress(macAddress);

            foreach (var pressureSensor in pressureSensorList)
            {
                _pressureSensorService.Remove(pressureSensor.Id);
            }

            return NoContent();
        }
    }
}