﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SensorDataGen.Classes.Sensors
{
    [Serializable]
    class Wind : Sensor
    {

        private int minValue = 0;
        private int maxValue = 500;

        // kmH 0 - 500
        public int speed { get; set; }
        // degree 0 - 360
        public int direction { get; set; }

        public Wind()
        {
            dataGenSpeed = GetDataPerSec(10);
            macAddress = GetRandomMacAddress();
            speed = GenerateRandomValue();
            direction = GenerateRandomValue(0, 360);
            sensorType = "wind";
        }

        public Wind(int minValue, int maxValue, int dataPerSec)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            dataGenSpeed = GetDataPerSec(dataPerSec);

            macAddress = GetRandomMacAddress();
            speed = GenerateRandomValue();
            direction = GenerateRandomValue(0, 360);
            sensorType = "wind";
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
            speed = GenerateRandomValue();
        }
        public override string ToString()
        {
            return $"{macAddress};{sensorType};{speed};{direction}";
        }

    }
}
