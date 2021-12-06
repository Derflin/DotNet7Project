using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using System.Threading;

namespace SensorDataGen.Classes
{
    class RabbitMQController
    {
        IConnection _connection;
        IModel _channel;
        
        public RabbitMQController()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            int retries = 5;
            while (true)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    break;
                }
                catch
                {
                    retries--;
                    if (retries == 0)
                        throw;

                    Thread.Sleep(2000);
                }
            }

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: "sensorData",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        public void SendData(string data)
        {
            var body = Encoding.UTF8.GetBytes(data);
            _channel.BasicPublish(exchange: "",
                routingKey: "sensorData",
                basicProperties: null,
                body: body);
            Console.WriteLine(" [x] Published {0} to RabbitMQ", data);
        }
    }
}
