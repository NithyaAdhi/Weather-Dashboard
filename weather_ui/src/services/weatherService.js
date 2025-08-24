import axios from "axios";

const API_URL = "https://localhost:7208/api/Weather";

const getWeatherData = async (token) => {
  try {
    const response = await axios.get(API_URL, {
      headers: {
        Authorization: `Bearer ${token}`, // Include the token in the Authorization header
      },
    });

    return response.data.map((city) => ({
      name: city.name,
      country: city.country,
      time: city.time,
      icon: city.icon,
      description: city.description,
      temp: city.temp,
      temp_min: city.temp_min,
      temp_max: city.temp_max,
      pressure: city.pressure,
      humidity: city.humidity,
      visibility: city.visibility,
      wind_speed: city.wind_speed,
      wind_deg: city.wind_deg,
      sunrise: city.sunrise,
      sunset: city.sunset,
    }));
  } catch (error) {
    console.error("Failed to fetch weather data", error);
    throw error;
  }
};

export { getWeatherData };
