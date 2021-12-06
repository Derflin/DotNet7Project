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
        public ActionResult<PaginatedListSensor<WindSensor>> Get(string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            _logger.LogInformation("Get request acquired for paginated list of wind sensors");
            List<WindSensor> items = _windSensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<WindSensor> paginatedList =
                new PaginatedListSensor<WindSensor>(items, items.Count, page, size);
            return paginatedList;
        }
        
        [FormatFilter]
        [HttpGet("{format}")]
        public ActionResult<List<WindSensor>> GetFilter(string format, string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            _logger.LogInformation($"Get request acquired for formatted \"{format}\" list of wind sensors");
            List<WindSensor> items = _windSensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<WindSensor> paginatedList =
                new PaginatedListSensor<WindSensor>(items, items.Count, page, size);
            return paginatedList.Items;
        }

        /*
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
        */
    }
}