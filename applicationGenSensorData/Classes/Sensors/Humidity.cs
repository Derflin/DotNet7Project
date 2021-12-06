using System;
using System.Collections.Generic;
using System.Text;

namespace SensorDataGen.Classes.Sensors
{
    [Serializable]
    class Humidity : Sensor
    {
        private double minValue = 0;
        private double maxValue = 100;

        // 0-100%
        public double humidity { get; set; }

        public Humidity()
        {
            dataGenSpeed = GetDataPerSec(dataPerSec);
            macAddress = GetRandomMacAddress();
            humidity = GenerateRandomValue();
            sensorType = "humidity";
        }

        public Humidity(double minValue, double maxValue, int dataPerSec)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.dataPerSec = dataPerSec;

            dataGenSpeed = GetDataPerSec(dataPerSec);
            macAddress = GetRandomMacAddress();
            humidity = GenerateRandomValue();
            sensorType = "humidity";
        }

        public double GenerateRandomValue()
        {
            var random = new Random();
            return Math.Round(((random.NextDouble()) * (this.maxValue - this.minValue) + this.minValue), 2);
        }
        public double GenerateRandomValue(double minValue, double maxValue)
        {
            var random = new Random();
            return Math.Round(((random.NextDouble()) * (maxValue - minValue) + minValue), 2);
        }
        public override void GenerateNewValue()
        {
            humidity = GenerateRandomValue();
        }
        public override object GetMaxValue()
        {
            return this.maxValue;
        }

        public override object GetMinValue()
        {
            return this.minValue;
        }

        public override string ToString()
        {
            return $"{macAddress};{sensorType};{humidity}";
        }

    }
}
