using System.Collections.Generic;
using applicationApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace applicationApi.Services
{
    public class HumiditySensorService
    {
        private readonly IMongoCollection<HumiditySensor> _humiditySensors;

        public HumiditySensorService(ISensorsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _humiditySensors = database.GetCollection<HumiditySensor>(settings.HumiditySensorsCollectionName);
        }

        public List<HumiditySensor> Get(string filterMacAddress = null, string sort = null, string order = null)
        {
            var findQuery = _humiditySensors.Find(humidity => 
                filterMacAddress == null || humidity.MacAddress == filterMacAddress);
            /*if (filterMacAddress == null)
            {
                findQuery = _humiditySensors.Find(humidity => true);
            }
            else
            {
                findQuery = _humiditySensors.Find(humiditySensor => humiditySensor.MacAddress == filterMacAddress);
            }*/
            findQuery = SortQuery(findQuery, sort, order);
            return findQuery.ToList();
        }

        private IFindFluent<HumiditySensor, HumiditySensor> SortQuery(
            IFindFluent<HumiditySensor, HumiditySensor> findQuery, 
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
                    findQuery.SortBy(bson => bson.Humidity);
                }
                else //if (order == null || order = "desc")
                {
                    findQuery.SortByDescending(bson => bson.Humidity);
                }
            }
            return findQuery;
        }
        
        public List<string> GetDistinctMacAddresses() =>
            _humiditySensors.Distinct<string>("MacAddress", new BsonDocument()).ToList();
        
        public HumiditySensor Create(HumiditySensor humiditySensor)
        {
            _humiditySensors.InsertOne(humiditySensor);
            return humiditySensor;
        }

        public void Update(string id, HumiditySensor humiditySensorIn)
        {
            _humiditySensors.ReplaceOne(humiditySensor => humiditySensor.Id == id, humiditySensorIn);
        }

        public void Remove(HumiditySensor humiditySensorIn)
        {
            _humiditySensors.DeleteOne(humiditySensor => humiditySensor.Id == humiditySensorIn.Id);
        }

        public void Remove(string id)
        {
            _humiditySensors.DeleteOne(humiditySensor => humiditySensor.Id == id);
        }

        public void RemoveAll()
        {
            _humiditySensors.DeleteMany(humiditySensor => true);
        }
    }
}