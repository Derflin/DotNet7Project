namespace applicationApi.Models
{
    public class TemperatureSensor : Sensor
    {
        public double Celsius { get; set; }
        
        public double Fahrenheit { get; set; }

        public TemperatureSensor()
        {
            this.Type = "Temperature";
        }
    }
}