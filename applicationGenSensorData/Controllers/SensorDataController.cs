using System;
using System.Text;
using applicationApi.Services;
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
        private readonly IMessageService _messageService;

        public SensorDataController(ILogger<SensorDataController> logger, IMessageService messageService)
        {
            this._logger = logger;
            this._messageService = messageService;
        }

        /*
         * Function to send data to RabbitMQ
         * Example based on:
         * https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
         * https://medium.com/trimble-maps-engineering-blog/getting-started-with-net-core-docker-and-rabbitmq-part-3-66305dc50ccf
         */
        [HttpPost]
        public IActionResult Post()
        {
            string message = "hello there";
            _messageService.Enqueue(message);

            return Ok("{\"success\": \"true\"}");
        }
    }
}