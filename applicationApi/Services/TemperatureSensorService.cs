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

        public List<TemperatureSensor> Get(int page, int size, string filterMacAddress = null, string sort = null, string order = null)
        {
            IFindFluent<TemperatureSensor, TemperatureSensor> findQuery = null;
            if (filterMacAddress == null)
            {
                findQuery = _temperatureSensors.Find(temperature => true);
            }
            else
            {
                findQuery = _temperatureSensors.Find(temperatureSensor => temperatureSensor.MacAddress == filterMacAddress);
            }
            findQuery = SortQuery(findQuery, sort, order);
            return findQuery.ToList();
        }

        private IFindFluent<TemperatureSensor, TemperatureSensor> SortQuery(
            IFindFluent<TemperatureSensor, TemperatureSensor> findQuery, 
            string sort, 
            string order) 
        {
            if (sort == null || sort == "date")
            {
                if (order != null && order == "asc")
                {
                    findQuery.SortBy(bson => bson.DateTime);
                }
                else //if (order == null || order = "desc")
                {
                    findQuery.SortByDescending(bson => bson.DateTime);
                }
            } else if (sort == "value")
            {
                if (order != null && order == "asc")
                {
                    findQuery.SortBy(bson => bson.Celsius);
                }
                else //if (order == null || order = "desc")
                {
                    findQuery.SortByDescending(bson => bson.Celsius);
                }
            }
            return findQuery;
        }
        
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