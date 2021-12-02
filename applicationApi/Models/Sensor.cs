using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace applicationApi.Models
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string MacAddress { get; set; }

        public DateTime DateTime { get; set; }
        
        public string Type { get; set; }
    }
}