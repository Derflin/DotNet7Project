using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace applicationApi.Models
{
    public class SensorWind
    {
        [BsonId]
        public string Timestamp { get; set; }
        
        
    }
}