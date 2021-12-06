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
        public ActionResult<PaginatedListSensor<PressureSensor>> Get(string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            _logger.LogInformation("Get request acquired for paginated list of pressure sensors");
            List<PressureSensor> items = _pressureSensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<PressureSensor> paginatedList =
                new PaginatedListSensor<PressureSensor>(items, items.Count, page, size);
            return paginatedList;
        }
        
        [FormatFilter]
        [HttpGet("{format}")]
        public ActionResult<List<PressureSensor>> GetFilter(string format, string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            _logger.LogInformation($"Get request acquired for formatted \"{format}\" list of pressure sensors");
            List<PressureSensor> items = _pressureSensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<PressureSensor> paginatedList =
                new PaginatedListSensor<PressureSensor>(items, items.Count, page, size);
            return paginatedList.Items;
        }

        /*
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
        */
    }
}