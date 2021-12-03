using System.Dynamic;

namespace applicationApi.Models
{
    public class SensorsDatabaseSettings : ISensorsDatabaseSettings
    {
        public string WindSensorsCollectionName { get; set; }
        public string PressureSensorsCollectionName { get; set; }
        public string TemperatureSensorsCollectionName { get; set; }
        public string HumiditySensorsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISensorsDatabaseSettings
    {
        string WindSensorsCollectionName { get; set; }
        string PressureSensorsCollectionName { get; set; }
        string TemperatureSensorsCollectionName { get; set; }
        string HumiditySensorsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}