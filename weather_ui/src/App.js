// src/App.js
import React, { useState, useEffect } from "react";
import "./App.css";
import { getWeatherData } from "./services/weatherService";
import WeatherCard from "./components/WeatherCard/WeatherCard";
import { WiCloud } from "react-icons/wi";
import AuthenticationButton from "./components/AuthenticationButton/AuthenticationButton";
import { useAuth0 } from "@auth0/auth0-react";

function App() {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [weatherData, setWeatherData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (!isAuthenticated) {
      return;
    }

    const fetchWeather = async () => {
      try {
        setLoading(true);
        const token = await getAccessTokenSilently(); // Get the token
        const data = await getWeatherData(token); // Pass the token to the service
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
  }, [isAuthenticated, getAccessTokenSilently]); // Re-run when authentication state changes

  return (
    <div className="App">
      <header className="app-header">
        <WiCloud size={50} />
        <h1>Weather App</h1>
         <AuthenticationButton /> 
      </header>

       {!isAuthenticated && (
        <h2>Please log in to see the weather dashboard.</h2>
      )}

      {isAuthenticated && loading && <p>Loading weather data...</p>}
      {isAuthenticated && error && <p style={{ color: "red" }}>{error}</p>}

      {isAuthenticated && !loading && !error && (
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
