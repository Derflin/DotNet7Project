using System;
using System.Collections.Generic;
using System.Text;

namespace SensorDataGen.Classes.Sensors
{
    [Serializable]
    class Pressure : Sensor
    {
        private int minValue = 800;
        private int maxValue = 1100;

        // hPa 1000 hPa 800 - 1100
        public int pressure { get; set; }

        public Pressure()
        {
            dataGenSpeed = GetDataPerSec(dataPerSec);
            macAddress = GetRandomMacAddress();
            pressure = GenerateRandomValue();
            sensorType = "pressure";
        }
        public Pressure(int minValue, int maxValue, int dataPerSec)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.dataPerSec = dataPerSec;

            dataGenSpeed = GetDataPerSec(dataPerSec);
            macAddress = GetRandomMacAddress();
            pressure = GenerateRandomValue();
            sensorType = "pressure";
        }
        public int GenerateRandomValue()
        {
            var random = new Random();
            return random.Next(minValue, maxValue + 1);
        }
        public int GenerateRandomValue(int minValue, int maxValue)
        {
            var random = new Random();
            return random.Next(minValue, maxValue + 1);
        }
        public override void GenerateNewValue()
        {
            pressure = GenerateRandomValue();
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
            return $"{macAddress};{sensorType};{pressure}";
        }
    }
}
