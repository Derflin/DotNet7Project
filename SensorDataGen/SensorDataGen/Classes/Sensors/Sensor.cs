using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorDataGen.Classes.Sensors
{
    abstract class Sensor
    {
        public string macAddress;
        public string sensorType;
        public int dataGenSpeed; //ms

        public static string GetRandomMacAddress()
        {
            var random = new Random();
            var buffer = new byte[6];
            random.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
            return result.TrimEnd(':');
        }

        public static int GetDataPerSec(int numOfData)
        {
            int dataPerSec = 1000 / numOfData;
            return dataPerSec == 0 ? 1 : dataPerSec;
        }
        public abstract void GenerateNewValue();
    }
}
