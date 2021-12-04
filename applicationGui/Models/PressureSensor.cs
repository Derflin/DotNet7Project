using System;
using System.Runtime.Serialization;
using applicationGui.Models;

namespace applicationGui.Models
{
    [DataContract, KnownType(typeof(Sensor))]
    public class PressureSensor : Sensor
    {
        [DataMember(Name = "pressure")]
        public int Pressure { get; set; }

        public PressureSensor() : base()
        {
            this.Type = "Pressure";
        }

        public PressureSensor(string macAddress, DateTime dateTime, string type, int pressure) : base(macAddress, dateTime, type)
        {
            Pressure = pressure;
        }
        
    }
}