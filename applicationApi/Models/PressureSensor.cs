namespace applicationApi.Models
{
    public class PressureSensor : Sensor
    {
        public int Pressure { get; set; }

        public PressureSensor()
        {
            this.Type = "Pressure";
        }
    }
}