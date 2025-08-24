
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using WeatherApi.Models; 

namespace WeatherApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "weatherData";

        public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<IEnumerable<WeatherResponse>> GetWeatherDataAsync()
        {
      
            if (_cache.TryGetValue(CacheKey, out IEnumerable<WeatherResponse> cachedData))
            {
                Console.WriteLine("Serving from cache.");
                return cachedData;
            }

            Console.WriteLine("Fetching new data.");

            var jsonText = await File.ReadAllTextAsync("cities.json");
            var cityList = JsonSerializer.Deserialize<CityList>(jsonText);
            var cityCodes = cityList.List.Select(c => c.CityCode).ToList();

            var apiKey = _configuration["OpenWeather:ApiKey"];
            var client = _httpClientFactory.CreateClient();

            var tasks = new List<Task<WeatherResponse>>();
            foreach (var cityId in cityCodes)
            {
                var apiUrl = $"http://api.openweathermap.org/data/2.5/weather?id={cityId}&units=metric&appid={apiKey}";
                tasks.Add(FetchAndParseSingleCityWeatherAsync(client, apiUrl));
            }

            var results = await Task.WhenAll(tasks);
            var weatherData = results.Where(r => r != null).ToList();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(CacheKey, weatherData, cacheEntryOptions);

            return weatherData;
        }


        /// Helper method to fetch and parse weather data for a single city URL.

        private async Task<WeatherResponse> FetchAndParseSingleCityWeatherAsync(HttpClient client, string url)
        {
            try
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to fetch weather for URL: {url}. Status: {response.StatusCode}");
                    return null;
                }

                var responseStream = await response.Content.ReadAsStreamAsync();
                var openWeatherData = await JsonSerializer.DeserializeAsync<JsonElement>(responseStream);

                // to match the WeatherResponse model and the frontend's needs.
                return new WeatherResponse
                {
                    Name = openWeatherData.GetProperty("name").GetString(),
                    Country = openWeatherData.GetProperty("sys").GetProperty("country").GetString(),
                    Time = openWeatherData.GetProperty("dt").GetInt64(),
                    Icon = openWeatherData.GetProperty("weather")[0].GetProperty("icon").GetString(),
                    Description = openWeatherData.GetProperty("weather")[0].GetProperty("description").GetString(),
                    Temp = openWeatherData.GetProperty("main").GetProperty("temp").GetDouble(),
                    Temp_Min = openWeatherData.GetProperty("main").GetProperty("temp_min").GetDouble(),
                    Temp_Max = openWeatherData.GetProperty("main").GetProperty("temp_max").GetDouble(),
                    Pressure = openWeatherData.GetProperty("main").GetProperty("pressure").GetInt32(),
                    Humidity = openWeatherData.GetProperty("main").GetProperty("humidity").GetInt32(),
                    Visibility = openWeatherData.GetProperty("visibility").GetInt32(),
                    Wind_Speed = openWeatherData.GetProperty("wind").GetProperty("speed").GetDouble(),
                    Wind_Deg = openWeatherData.GetProperty("wind").GetProperty("deg").GetInt32(),
                    Sunrise = openWeatherData.GetProperty("sys").GetProperty("sunrise").GetInt64(),
                    Sunset = openWeatherData.GetProperty("sys").GetProperty("sunset").GetInt64()
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred for URL {url}: {ex.Message}");
                return null;
            }
        }
    }
}