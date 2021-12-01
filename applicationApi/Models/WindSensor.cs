using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace applicationApi.Models
{
    // TODO: try to add inheritence - for example inheritance from Sensor class with MacAddress and Timestamp
    public class WindSensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string MacAddress { get; set; }

        public string Timestamp { get; set; }
        
        public int Speed { get; set; }
        
        public int Direction { get; set; }
    }
}