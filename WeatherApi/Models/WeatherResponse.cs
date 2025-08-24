namespace WeatherApi.Models
{

public class WeatherResponse
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public long Time { get; set; } // dt from API is a Unix timestamp (long)
        public string Icon { get; set; }
        public string Description { get; set; }
        public double Temp { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Visibility { get; set; }
        public double Wind_Speed { get; set; }
        public int Wind_Deg { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
    }
}
