

using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using WeatherApi.Models;
using System.Text.Json;
using System.Threading.Tasks;

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
                // Step 1: Try to get data from cache first
                if (_cache.TryGetValue(CacheKey, out IEnumerable<WeatherResponse> cachedData))
                {
                    Console.WriteLine("Serving from cache.");
                    return cachedData;
                }

                Console.WriteLine("Fetching new data.");

                // Step 2: Read city codes from the JSON file
                var jsonText = await File.ReadAllTextAsync("cities.json");
                var cityList = JsonSerializer.Deserialize<CityList>(jsonText);
                var cityCodes = cityList.List.Select(c => c.CityCode).ToList();

                var apiKey = _configuration["OpenWeather:ApiKey"];
                var client = _httpClientFactory.CreateClient();

                // Step 3: Create a list of tasks to fetch weather for each city concurrently
                var tasks = new List<Task<WeatherResponse>>();
                foreach (var cityId in cityCodes)
                {
                    // Use the /weather endpoint which works with the free API key
                    var apiUrl = $"http://api.openweathermap.org/data/2.5/weather?id={cityId}&units=metric&appid={apiKey}";
                    tasks.Add(FetchAndParseSingleCityWeatherAsync(client, apiUrl));
                }

                // Step 4: Wait for all the individual API calls to complete
                var results = await Task.WhenAll(tasks);

                // Filter out any null results that may have occurred from failed individual calls
                var weatherData = results.Where(r => r != null).ToList();

                // Step 5: Store the final compiled list in the cache
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(CacheKey, weatherData, cacheEntryOptions);

                return weatherData;
            }

            /// <summary>
            /// Helper method to fetch and parse weather data for a single city URL.
            /// This isolates the logic and allows for graceful failure of individual API calls.
            /// </summary>
            /// <param name="client">The HttpClient instance to use.</param>
            /// <param name="url">The specific API URL for one city.</param>
            /// <returns>A WeatherResponse object, or null if the call fails.</returns>
            private async Task<WeatherResponse> FetchAndParseSingleCityWeatherAsync(HttpClient client, string url)
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to fetch weather for URL: {url}. Status: {response.StatusCode}");
                        return null; // Return null to indicate failure for this specific city
                    }

                    var responseStream = await response.Content.ReadAsStreamAsync();
                    var openWeatherData = await JsonSerializer.DeserializeAsync<JsonElement>(responseStream);

                    // Map the fields from the single-city response structure
                    return new WeatherResponse
                    {
                        Name = openWeatherData.GetProperty("name").GetString(),
                        Temp = openWeatherData.GetProperty("main").GetProperty("temp").GetDouble(),
                        Weather = openWeatherData.GetProperty("weather")[0].GetProperty("description").GetString()
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred for URL {url}: {ex.Message}");
                    return null; // Return null on exception
                }
            }
        }
    
}
