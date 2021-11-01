using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace applicationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly ILogger<SensorDataController> _logger;

        public SensorDataController(ILogger<SensorDataController> logger)
        {
            this._logger = logger;
        }
        
        [HttpGet]
        public String Get()
        {
            return "hello";
        }
    }
}