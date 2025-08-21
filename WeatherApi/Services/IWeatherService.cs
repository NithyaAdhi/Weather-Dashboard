using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherResponse>> GetWeatherDataAsync();
    }
}
