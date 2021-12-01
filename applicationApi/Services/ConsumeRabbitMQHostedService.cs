using System;
using System.Threading;
using System.Threading.Tasks;
using applicationApi.Controllers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace applicationApi.Services
{
    /*
     * Based on example from:
     * https://www.c-sharpcorner.com/article/consuming-rabbitmq-messages-in-asp-net-core/
     * and
     * https://medium.com/trimble-maps-engineering-blog/getting-started-with-net-core-docker-and-rabbitmq-part-3-66305dc50ccf
     */
    public class ConsumeRabbitMQHostedService : BackgroundService
    {
        private readonly ILogger _logger;  
        private IConnection _conn;  
        private IModel _channel;  
      
        public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory)  
        {
            this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
            InitRabbitMQ();  
        }  
      
        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "rabbit", UserName = "guest", Password = "guest" };
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
            _channel = _conn.CreateModel();  
            _channel.QueueDeclare(queue: "sensorData",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            _channel.BasicQos(0, 1, false);  
      
            _conn.ConnectionShutdown += RabbitMQ_ConnectionShutdown;  
        }  
      
        protected override Task ExecuteAsync(CancellationToken stoppingToken)  
        {  
            stoppingToken.ThrowIfCancellationRequested();  
      
            var consumer = new EventingBasicConsumer(_channel);  
            consumer.Received += (ch, ea) =>  
            {  
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());  
      
                // handle the received message  
                HandleMessage(content);  
                _channel.BasicAck(ea.DeliveryTag, false);  
            };  
      
            consumer.Shutdown += OnConsumerShutdown;  
            consumer.Registered += OnConsumerRegistered;  
            consumer.Unregistered += OnConsumerUnregistered;  
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
      
            _channel.BasicConsume("sensorData", false, consumer);  
            return Task.CompletedTask;  
        }  
      
        private void HandleMessage(string content)  
        {
            // TODO: handle the message
            _logger.LogInformation($"consumer received {content}");  
        }  
          
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
      
        public override void Dispose()  
        {  
            _channel.Close();  
            _conn.Close();  
            base.Dispose();  
        }  
    }
}