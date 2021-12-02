﻿using System;
using System.Threading;
using System.Threading.Tasks;
using applicationApi.Controllers;
using applicationApi.Models;
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
      
        private readonly HumiditySensorService _humiditySensorService;
        private readonly PressureSensorService _pressureSensorService;
        private readonly TemperatureSensorService _temperatureSensorService;
        private readonly WindSensorService _windSensorService;
        
        public ConsumeRabbitMQHostedService(ILoggerFactory loggerFactory, HumiditySensorService humiditySensorService,
            PressureSensorService pressureSensorService, TemperatureSensorService temperatureSensorService,
            WindSensorService windSensorService)  
        {
            this._logger = loggerFactory.CreateLogger<ConsumeRabbitMQHostedService>();
            InitRabbitMQ();
            this._humiditySensorService = humiditySensorService;
            this._pressureSensorService = pressureSensorService;
            this._temperatureSensorService = temperatureSensorService;
            this._windSensorService = windSensorService;
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
            _logger.LogInformation($"consumer received: {content}");
            string[] data = content.Split(";");
            if (data.Length > 2)
            {
                string type = data[1];
                switch (type)
                {
                    case "humidity":
                        var humiditySensor = new HumiditySensor();
                        humiditySensor.Timestamp = GetTimestamp(DateTime.Now);
                        humiditySensor.MacAddress = data[0];
                        humiditySensor.Humidity = Double.Parse(data[2]);
                        _humiditySensorService.Create(humiditySensor);
                        break;
                    case "pressure":
                        var pressureSensor = new PressureSensor();
                        pressureSensor.Timestamp = GetTimestamp(DateTime.Now);
                        pressureSensor.MacAddress = data[0];
                        pressureSensor.Pressure = Int32.Parse(data[2]);
                        _pressureSensorService.Create(pressureSensor);
                        break;
                    case "temperature":
                        var temperatureSensor = new TemperatureSensor();
                        temperatureSensor.Timestamp = GetTimestamp(DateTime.Now);
                        temperatureSensor.MacAddress = data[0];
                        temperatureSensor.Celsius = Double.Parse(data[2]);
                        temperatureSensor.Fahrenheit = Double.Parse(data[3]);
                        _temperatureSensorService.Create(temperatureSensor);
                        break;
                    case "wind":
                        var windSensor = new WindSensor();
                        windSensor.Timestamp = GetTimestamp(DateTime.Now);
                        windSensor.MacAddress = data[0];
                        windSensor.Speed = Int32.Parse(data[2]);
                        windSensor.Direction = Int32.Parse(data[3]);
                        _windSensorService.Create(windSensor);
                        break;
                }
            }
        }  
          
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
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