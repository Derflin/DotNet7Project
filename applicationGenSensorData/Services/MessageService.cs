using System;
using System.Text;
using RabbitMQ.Client;

namespace applicationApi.Services
{
    // define interface and service
    public interface IMessageService
    {
        bool Enqueue(string message);
    }

    public class MessageService : IMessageService
    {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        public MessageService()
        {
            Console.WriteLine("about to connect to rabbit");

            _factory = new ConnectionFactory() { HostName = "rabbit", UserName = "guest", Password = "guest", Port = 5672 };
            _conn = _factory.CreateConnection();
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