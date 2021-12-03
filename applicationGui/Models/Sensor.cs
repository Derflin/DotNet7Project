using System;
using System.Runtime.Serialization;

namespace applicationGui.Models
{
    [DataContract]
    public class Sensor
    {
   
        [DataMember(Name = "id")] public string Id { get; set; }
        
        [DataMember(Name = "macAddress")] public string MacAddress { get; set; }

        [DataMember(Name = "dateTime")] public DateTime DateTime { get; set; }
        
        [DataMember(Name = "type")] public string Type { get; set; }
        
        public Sensor(string id, string macAddress, DateTime dateTime, string type)
        {
            Id = id;
            MacAddress = macAddress;
            DateTime = dateTime;
            Type = type;
        }

        public Sensor()
        {
        }
    }
}