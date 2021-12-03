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

        public List<HumiditySensor> Get() =>
            _humiditySensors.Find(humidity => true).ToList();

        public HumiditySensor Get(string id) =>
            _humiditySensors.Find<HumiditySensor>(humiditySensor => humiditySensor.Id == id).FirstOrDefault();

        public List<HumiditySensor> GetByMacAddress(string macAddress) =>
            _humiditySensors.Find<HumiditySensor>(humiditySensor => humiditySensor.MacAddress == macAddress).ToList();
        
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