using System.Collections.Generic;
using applicationApi.Models;
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