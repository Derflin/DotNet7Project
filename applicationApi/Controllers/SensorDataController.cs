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

        /*
         * Function to send data to RabbitMQ
         * Example based on:
         * https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
         */
        [HttpPost]
        [Route("SendData")]
        public IActionResult Post()
        {
            var factory = new ConnectionFactory(){HostName = "rabbit", UserName = "guest", Password = "guest"};
            using(var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // TODO: Change queue name
                channel.QueueDeclare(queue: "test",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = "Hello there traveler";
                var body = Encoding.UTF8.GetBytes(message);
                
                channel.BasicPublish(exchange: "",
                    // TODO: Change queue name
                    routingKey: "test",
                    basicProperties: null,
                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            return RedirectToAction("");
        }
    }
}