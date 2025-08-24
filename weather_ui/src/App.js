// src/App.js
import React, { useState, useEffect } from "react";
import "./App.css";
import { getWeatherData } from "./services/weatherService";
import WeatherCard from "./components/WeatherCard/WeatherCard";
import { WiCloud } from "react-icons/wi";

function App() {
  const [weatherData, setWeatherData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchWeather = async () => {
      try {
        setLoading(true);
        const data = await getWeatherData();
        setWeatherData(data);
        setError(null);
      } catch (err) {
        setError(
          "Failed to load weather data. Please ensure the backend is running."
        );
      } finally {
        setLoading(false);
      }
    };

    fetchWeather();
  }, []); // Empty array means this runs once on component mount

  return (
    <div className="App">
      <header className="app-header">
        <WiCloud size={50} />
        <h1>Weather App</h1>
      </header>

      {loading && <p>Loading weather data...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}

      {!loading && !error && (
        <main className="weather-grid">
          {weatherData.map((city, index) => (
            <WeatherCard key={city.name} data={city} colorIndex={index} />
          ))}
        </main>
      )}

      <footer className="app-footer">© 2025 weather data</footer>
    </div>
  );
}

export default App;
