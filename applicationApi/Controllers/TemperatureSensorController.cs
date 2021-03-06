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
        public ActionResult<PaginatedListSensor<TemperatureSensor>> Get(string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            _logger.LogInformation("Get request acquired for paginated list of temperature sensors");
            List<TemperatureSensor> items = _temperatureSensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<TemperatureSensor> paginatedList =
                new PaginatedListSensor<TemperatureSensor>(items, items.Count, page, size);
            return paginatedList;
        }
        
        [FormatFilter]
        [HttpGet("{format}")]
        public ActionResult<List<TemperatureSensor>> GetFilter(string format, string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            _logger.LogInformation($"Get request acquired for formatted \"{format}\" list of temperature sensors");
            List<TemperatureSensor> items = _temperatureSensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<TemperatureSensor> paginatedList =
                new PaginatedListSensor<TemperatureSensor>(items, items.Count, page, size);
            return paginatedList.Items;
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