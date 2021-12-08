using System;
using System.Collections.Generic;
using System.Text;

namespace SensorDataGen.Classes.Sensors
{
    [Serializable]
    class Temperature : Sensor
    {
        private double minValue = -100;
        private double maxValue = 100;

        // <-100, 100>
        public double celsius { get; set; }
        public double fahrenheit { get; set; }

        public Temperature()
        {
            dataGenSpeed = GetDataPerSec(dataPerSec);
            macAddress = GetRandomMacAddress();
            celsius = GenerateRandomValue();
            fahrenheit = CelsiusToFahrenheit(celsius);
            sensorType = "temperature";
        }
        public Temperature(string mac, double minValue, double maxValue, int dataPerSec)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.dataPerSec = dataPerSec;

            if (mac.Length == 0)
                macAddress = GetRandomMacAddress();
            else
                macAddress = mac;

            dataGenSpeed = GetDataPerSec(dataPerSec);
            celsius = GenerateRandomValue();
            fahrenheit = CelsiusToFahrenheit(celsius);
            sensorType = "temperature";
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
            celsius = GenerateRandomValue();
            fahrenheit = CelsiusToFahrenheit(celsius);
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
            return $"{macAddress};{sensorType};{celsius};{fahrenheit}";
        }

        private double CelsiusToFahrenheit(double celsius)
        {
            return Math.Round((celsius * 1.8 + 32), 2);
        }
    }
}
