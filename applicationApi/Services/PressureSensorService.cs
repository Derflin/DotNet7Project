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

        public List<PressureSensor> Get(string filterMacAddress = null, string sort = null, string order = null)
        {
            var findQuery = _pressureSensors.Find(pressure => 
                filterMacAddress == null || pressure.MacAddress == filterMacAddress);
            /*if (filterMacAddress == null)
            {
                findQuery = _pressureSensors.Find(pressure => true);
            }
            else
            {
                findQuery = _pressureSensors.Find(pressureSensor => pressureSensor.MacAddress == filterMacAddress);
            }*/
            findQuery = SortQuery(findQuery, sort, order);
            return findQuery.ToList();
        }

        private IFindFluent<PressureSensor, PressureSensor> SortQuery(
            IFindFluent<PressureSensor, PressureSensor> findQuery, 
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
                    findQuery.SortBy(bson => bson.Pressure);
                }
                else //if (order == null || order = "desc")
                {
                    findQuery.SortByDescending(bson => bson.Pressure);
                }
            }
            return findQuery;
        }

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