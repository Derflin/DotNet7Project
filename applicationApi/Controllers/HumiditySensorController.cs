using System.Collections.Generic;
using applicationApi.Models;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("api/sensors/humidity")]
    public class HumiditySensorController : Controller
    {
        private readonly ILogger<HumiditySensorController> _logger;
        private readonly HumiditySensorService _humiditySensorService;

        public HumiditySensorController(ILogger<HumiditySensorController> logger, HumiditySensorService humiditySensorService)
        {
            this._logger = logger;
            this._humiditySensorService = humiditySensorService;
        }
        
        [HttpGet]
        public ActionResult<List<HumiditySensor>> Get() =>
            _humiditySensorService.Get();

        [HttpGet("{macAddress:length(12)}", Name = "GetHumiditySensor")]
        public ActionResult<List<HumiditySensor>> Get(string macAddress) => 
            _humiditySensorService.GetByMacAddress(macAddress);

        [HttpDelete("{macAddress:length(12)}")]
        public IActionResult Delete(string macAddress)
        {
            var humiditySensorList = _humiditySensorService.GetByMacAddress(macAddress);

            foreach (var humiditySensor in humiditySensorList)
            {
                _humiditySensorService.Remove(humiditySensor.Id);
            }

            return NoContent();
        }
    }
}