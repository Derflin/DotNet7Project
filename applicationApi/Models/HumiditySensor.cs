namespace applicationApi.Models
{
    public class HumiditySensor : Sensor
    {
        public double Humidity { get; set; }

        public HumiditySensor()
        {
            this.Type = "Humidity";
        }
    }
}