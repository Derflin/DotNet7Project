using System.Collections.Generic;
using applicationApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace applicationApi.Services
{
    public class WindSensorService
    {
        private readonly IMongoCollection<WindSensor> _windSensors;

        public WindSensorService(ISensorsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _windSensors = database.GetCollection<WindSensor>(settings.WindSensorsCollectionName);
        }

        public List<WindSensor> Get() =>
            _windSensors.Find(windSensor => true).ToList();

        public WindSensor Get(string id) =>
            _windSensors.Find<WindSensor>(windSensor => windSensor.Id == id).FirstOrDefault();

        public List<WindSensor> GetByMacAddress(string macAddress) =>
            _windSensors.Find<WindSensor>(windSensor => windSensor.MacAddress == macAddress).ToList();

        public List<string> GetDistinctMacAddresses() =>
            _windSensors.Distinct<string>("MacAddress", new BsonDocument()).ToList();

        public WindSensor Create(WindSensor windSensor)
        {
            _windSensors.InsertOne(windSensor);
            return windSensor;
        }

        public void Update(string id, WindSensor windSensorIn)
        {
            _windSensors.ReplaceOne(windSensor => windSensor.Id == id, windSensorIn);
        }

        public void Remove(WindSensor windSensorIn)
        {
            _windSensors.DeleteOne(windSensor => windSensor.Id == windSensorIn.Id);
        }

        public void Remove(string id)
        {
            _windSensors.DeleteOne(windSensor => windSensor.Id == id);
        }
    }
}