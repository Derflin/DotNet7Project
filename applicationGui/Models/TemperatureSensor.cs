using System;
using System.Runtime.Serialization;

namespace applicationGui.Models
{
    [DataContract, KnownType(typeof(Sensor))]
    public class TemperatureSensor : Sensor
    {
        
        [DataMember(Name = "celsius")] public double Celsius { get; set; }
        
        [DataMember(Name = "fahrenheit")] public double Fahrenheit { get; set; }

        public TemperatureSensor() : base()
        {
            this.Type = "Temperature";
        }

        public TemperatureSensor(string macAddress, DateTime dateTime, string type, double celsius, double fahrenheit) : base(macAddress, dateTime, type)
        {
            Celsius = celsius;
            Fahrenheit = fahrenheit;
        }

        public TemperatureSensor(double celsius, double fahrenheit)
        {
            Celsius = celsius;
            Fahrenheit = fahrenheit;
        }
    }
}