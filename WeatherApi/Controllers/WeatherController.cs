using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var weatherData = await _weatherService.GetWeatherDataAsync();
            if (weatherData == null)
            {
                return StatusCode(500, "An error occurred while fetching weather data.");
            }
            return Ok(weatherData);
        }
    }
}
