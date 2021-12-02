using System.Collections.Generic;
using applicationApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace applicationApi.Services
{
    public class PressureSensorService
    {
        private readonly IMongoCollection<PressureSensor> _pressureSensors;

        public PressureSensorService(ISensorsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pressureSensors = database.GetCollection<PressureSensor>(settings.PressureSensorsCollectionName);
        }

        public List<PressureSensor> Get() =>
            _pressureSensors.Find(pressure => true).ToList();

        public PressureSensor Get(string id) =>
            _pressureSensors.Find<PressureSensor>(pressureSensor => pressureSensor.Id == id).FirstOrDefault();

        public List<PressureSensor> GetByMacAddress(string macAddress) =>
            _pressureSensors.Find<PressureSensor>(pressureSensor => pressureSensor.MacAddress == macAddress).ToList();
        
        public List<string> GetDistinctMacAddresses() =>
            _pressureSensors.Distinct<string>("MacAddress", new BsonDocument()).ToList();
        
        public PressureSensor Create(PressureSensor pressureSensor)
        {
            _pressureSensors.InsertOne(pressureSensor);
            return pressureSensor;
        }

        public void Update(string id, PressureSensor pressureSensorIn)
        {
            _pressureSensors.ReplaceOne(pressureSensor => pressureSensor.Id == id, pressureSensorIn);
        }

        public void Remove(PressureSensor pressureSensorIn)
        {
            _pressureSensors.DeleteOne(pressureSensor => pressureSensor.Id == pressureSensorIn.Id);
        }

        public void Remove(string id)
        {
            _pressureSensors.DeleteOne(pressureSensor => pressureSensor.Id == id);
        }

        public void RemoveAll()
        {
            _pressureSensors.DeleteMany(pressureSensor => true);
        }
    }
}