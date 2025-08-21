using System.Text.Json.Serialization;

namespace WeatherApi.Models
{
    public class City
    {
        [JsonPropertyName("CityCode")]
        public string CityCode { get; set; }
    }

    public class CityList
    {
        public List<City> List { get; set; }
    }

}
