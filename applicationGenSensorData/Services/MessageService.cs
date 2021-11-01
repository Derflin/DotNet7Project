using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace applicationApi.Services
{
    // define interface and service
    public interface IMessageService
    {
        bool Enqueue(string message);
    }

    public class MessageService : IMessageService
    {
        private IConnection _conn;
        private IModel _channel;
        public MessageService()
        {
            Console.WriteLine("about to connect to rabbit");

            var factory = new ConnectionFactory() { HostName = "rabbit", UserName = "guest", Password = "guest", Port = 5672 };
            /*
             * TODO: this below is a temporary code for retrying connection
             */
            int retries = 5;
            while (true)
            {
                try
                {
                    _conn = factory.CreateConnection();
                    break;
                }
                catch (BrokerUnreachableException e)
                {
                    retries--;
                    if (retries == 0) throw;
                    Thread.Sleep(2000);
                }
            }
            /*
             * 
             */
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "sensorData",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        public bool Enqueue(string messageString)
        {
            var body = Encoding.UTF8.GetBytes("server processed " + messageString);
            _channel.BasicPublish(exchange: "",
                routingKey: "sensorData",
                basicProperties: null,
                body: body);
            Console.WriteLine(" [x] Published {0} to RabbitMQ", messageString);
            return true;
        }
    }
}