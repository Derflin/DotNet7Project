using System.Collections.Generic;
using applicationApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace applicationApi.Services
{
    public class TemperatureSensorService
    {
        private readonly IMongoCollection<TemperatureSensor> _temperatureSensors;

        public TemperatureSensorService(ISensorsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _temperatureSensors = database.GetCollection<TemperatureSensor>(settings.TemperatureSensorsCollectionName);
        }

        public List<TemperatureSensor> Get() =>
            _temperatureSensors.Find(temperature => true).ToList();

        public TemperatureSensor Get(string id) =>
            _temperatureSensors.Find<TemperatureSensor>(temperatureSensor => temperatureSensor.Id == id).FirstOrDefault();

        public List<TemperatureSensor> GetByMacAddress(string macAddress) =>
            _temperatureSensors.Find<TemperatureSensor>(temperatureSensor => temperatureSensor.MacAddress == macAddress).ToList();
        
        public List<string> GetDistinctMacAddresses() =>
            _temperatureSensors.Distinct<string>("MacAddress", new BsonDocument()).ToList();
        
        public TemperatureSensor Create(TemperatureSensor temperatureSensor)
        {
            _temperatureSensors.InsertOne(temperatureSensor);
            return temperatureSensor;
        }

        public void Update(string id, TemperatureSensor temperatureSensorIn)
        {
            _temperatureSensors.ReplaceOne(temperatureSensor => temperatureSensor.Id == id, temperatureSensorIn);
        }

        public void Remove(TemperatureSensor temperatureSensorIn)
        {
            _temperatureSensors.DeleteOne(temperatureSensor => temperatureSensor.Id == temperatureSensorIn.Id);
        }

        public void Remove(string id)
        {
            _temperatureSensors.DeleteOne(temperatureSensor => temperatureSensor.Id == id);
        }

        public void RemoveAll()
        {
            _temperatureSensors.DeleteMany(temperatureSensor => true);
        }
    }
}