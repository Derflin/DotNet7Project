using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SensorDataGen.Classes.Sensors;
using System.Diagnostics;

namespace SensorDataGen.Classes.Controllers
{
    class SensorsController
    {
        private List<Sensor> sensors;
        private Thread dataGenThread;
        private RabbitMQController rabbitController;

        private bool mustStop;

        public SensorsController(RabbitMQController rabbitController)
        {
            sensors = new List<Sensor>();
            dataGenThread = null;
            mustStop = false;

            this.rabbitController = rabbitController;
        }
        public bool HasSensors()
        {
            if (sensors.Count != 0)
                return true;
            return false;
        }
        public Sensor AddSensor(string sensorType)
        {
            Sensor sensor = null;

            switch (sensorType)
            {
                case "wind":
                    sensor = new Wind();
                    break;
                case "temperature":
                    sensor = new Temperature();
                    break;
                case "pressure":
                    sensor = new Pressure();
                    break;
                case "humidity":
                    sensor = new Humidity();
                    break;
            }

            sensors.Add(sensor);
            return sensor;
        }
        public Sensor AddSensor(string mac, string sensorType, decimal minValue, decimal maxValue, int dataPerSec)
        {
            Sensor sensor = null;

            switch (sensorType)
            {
                case "wind":
                    sensor = new Wind(mac, decimal.ToInt32(minValue), decimal.ToInt32(maxValue), dataPerSec);
                    break;
                case "temperature":
                    sensor = new Temperature(mac, decimal.ToDouble(minValue), decimal.ToDouble(maxValue), dataPerSec);
                    break;
                case "pressure":
                    sensor = new Pressure(mac, decimal.ToInt32(minValue), decimal.ToInt32(maxValue), dataPerSec);
                    break;
                case "humidity":
                    sensor = new Humidity(mac, decimal.ToDouble(minValue), decimal.ToDouble(maxValue), dataPerSec);
                    break;
            }

            sensors.Add(sensor);
            return sensor;
        }
        public void DeleteSensor(object sensor)
        {
            sensors.Remove((Sensor)sensor);
        }

        public void StartGenerator()
        {
            dataGenThread = new Thread(GenerateData);
            dataGenThread.Start();
        }
        public void StopGenerator()
        {
            mustStop = true;
        }
        private void GenerateData()
        {
            long i = 0;
            long last = -1;

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while(true && i < 100000)
            {
                if (mustStop)
                    break;

                i = stopWatch.ElapsedMilliseconds;

                if (i != last)
                {
                    foreach (Sensor sensor in sensors)
                    {
                        if (i % sensor.dataGenSpeed == 0)
                        {
                            rabbitController.SendData(sensor.ToString());
                            sensor.GenerateNewValue();
                            last = i;
                        }
                    }
                }

            }
            stopWatch.Stop();
            mustStop = false;
        }
    }
}
