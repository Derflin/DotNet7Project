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

        public List<WindSensor> Get(int page, int size, string filterMacAddress = null, string sort = null, string order = null)
        {
            IFindFluent<WindSensor, WindSensor> findQuery = null;
            if (filterMacAddress == null)
            {
                findQuery = _windSensors.Find(wind => true);
            }
            else
            {
                findQuery = _windSensors.Find(windSensor => windSensor.MacAddress == filterMacAddress);
            }
            findQuery = SortQuery(findQuery, sort, order);
            return findQuery.ToList();
        }

        private IFindFluent<WindSensor, WindSensor> SortQuery(
            IFindFluent<WindSensor, WindSensor> findQuery, 
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
                    findQuery.SortBy(bson => bson.Speed);
                }
                else //if (order == null || order = "desc")
                {
                    findQuery.SortByDescending(bson => bson.Speed);
                }
            }
            return findQuery;
        }

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

        public void RemoveAll()
        {
            _windSensors.DeleteMany(windSensor => true);
        }
    }
}