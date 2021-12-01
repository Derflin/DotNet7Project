using System.Collections.Generic;
using applicationApi.Models;
using applicationApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        /*
        [HttpGet("{id:length(24)}", Name = "GetWindSensor")]
        public ActionResult<WindSensor> Get(string id)
        {
            var windSensor = _windSensorService.Get(id);

            if (windSensor == null)
            {
                return NotFound();
            }

            return windSensor;
        }
        */

        /*
        [HttpPost]
        public ActionResult<WindSensor> Create(WindSensor windSensor)
        {
            _windSensorService.Create(windSensor);

            return CreatedAtRoute("GetWindSensor", new { id = windSensor.Id.ToString() }, windSensor);
        }
        */

        /*
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, WindSensor windSensorIn)
        {
            var windSensor = _windSensorService.Get(id);

            if (windSensor == null)
            {
                return NotFound();
            }

            _windSensorService.Update(id, windSensorIn);

            return NoContent();
        }
        */

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
        
        /*
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var windSensor = _windSensorService.Get(id);

            if (windSensor == null)
            {
                return NotFound();
            }

            _windSensorService.Remove(windSensor.Id);

            return NoContent();
        }
        */
    }
}