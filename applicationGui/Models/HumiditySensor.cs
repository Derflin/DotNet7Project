using System;
using System.Runtime.Serialization;
using applicationGui.Models;

namespace applicationGui.Models
{
    [DataContract, KnownType(typeof(Sensor))]
    public class HumiditySensor : Sensor
    {
        [DataMember(Name = "humidity")] public double Humidity { get; set; }

        public HumiditySensor() : base()
        {
            this.Type = "Humidity";
        }

        public HumiditySensor(string id, string macAddress, DateTime dateTime, string type, double humidity) : base(id, macAddress, dateTime, type)
        {
            Humidity = humidity;
        }

    }
}