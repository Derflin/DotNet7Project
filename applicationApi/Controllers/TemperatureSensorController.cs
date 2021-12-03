using System.Collections.Generic;
using applicationApi.Models;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("api/sensors/temperature")]
    public class TemperatureSensorController : Controller
    {
        private readonly ILogger<TemperatureSensorController> _logger;
        private readonly TemperatureSensorService _temperatureSensorService;

        public TemperatureSensorController(ILogger<TemperatureSensorController> logger, TemperatureSensorService temperatureSensorService)
        {
            this._logger = logger;
            this._temperatureSensorService = temperatureSensorService;
        }
        
        [HttpGet]
        public ActionResult<PaginatedListSensor<TemperatureSensor>> Get(string address, int page, int size, string sort, string order)
        {
            List<TemperatureSensor> items = _temperatureSensorService.Get(page, size, address, sort, order);
            PaginatedListSensor<TemperatureSensor> paginatedList =
                new PaginatedListSensor<TemperatureSensor>(items, items.Count, page, size);
            return paginatedList;
        }

        /*
        [HttpDelete("{macAddress:length(12)}")]
        public IActionResult Delete(string macAddress)
        {
            var temperatureSensorList = _temperatureSensorService.GetByMacAddress(macAddress);

            foreach (var temperatureSensor in temperatureSensorList)
            {
                _temperatureSensorService.Remove(temperatureSensor.Id);
            }

            return NoContent();
        }
        */
    }
}