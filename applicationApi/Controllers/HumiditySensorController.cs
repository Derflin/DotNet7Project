using System;
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
        public ActionResult<PaginatedListSensor<HumiditySensor>> Get(string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            List<HumiditySensor> items = _humiditySensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<HumiditySensor> paginatedList =
                new PaginatedListSensor<HumiditySensor>(items, items.Count, page, size);
            return paginatedList;
        }
        
        [FormatFilter]
        [HttpGet("{format}")]
        public ActionResult<List<HumiditySensor>> GetFilter(string format, string address, string minDate, string maxDate, int page, int size, string sort, string order)
        {
            List<HumiditySensor> items = _humiditySensorService.Get(address, minDate, maxDate, sort, order);
            PaginatedListSensor<HumiditySensor> paginatedList =
                new PaginatedListSensor<HumiditySensor>(items, items.Count, page, size);
            return paginatedList.Items;
        }

        /*
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
        */
    }
}