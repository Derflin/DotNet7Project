using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace applicationApi.Models
{
    public class WindSensor : Sensor
    {
        public int Speed { get; set; }
        
        public int Direction { get; set; }

        public WindSensor()
        {
            this.Type = "Wind";
        }
    }
}