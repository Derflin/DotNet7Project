using System;
using System.Runtime.Serialization;

namespace applicationGui.Models
{
    [DataContract, KnownType(typeof(Sensor))]
    public class WindSensor : Sensor
    {
        [DataMember(Name = "speed")] public int Speed { get; set; }
        
        [DataMember(Name = "direction")] public int Direction { get; set; }

        public WindSensor() : base()
        {
            this.Type = "Wind";
        }

        public WindSensor(string macAddress, DateTime dateTime, string type, int speed, int direction) : base(macAddress, dateTime, type)
        {
            Speed = speed;
            Direction = direction;
        }

        public WindSensor(int speed, int direction)
        {
            Speed = speed;
            Direction = direction;
        }
    }
}