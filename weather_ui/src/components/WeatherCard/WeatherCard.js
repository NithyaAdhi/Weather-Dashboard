// src/components/WeatherCard/WeatherCard.js
import React from "react";
import styles from "./WeatherCard.module.css";
import { format } from "date-fns";
import { WiDirectionUp } from "react-icons/wi";

// Helper for dynamic background colors based on the design
const cardColors = ["#388ee7", "#6249cc", "#40b681", "#de934f", "#cc5050"];

// Helper to map API icon codes to a component
const getWeatherIcon = (iconCode) => {
  return `http://openweathermap.org/img/wn/${iconCode}@2x.png`;
};

const WeatherCard = ({ data, colorIndex }) => {
  // Dynamically select a color from our array
  const backgroundColor = cardColors[colorIndex % cardColors.length];

  // Format data using date-fns
  const formattedTime = format(new Date(data.time * 1000), "h:mmaaa");
  const formattedDate = format(new Date(data.time * 1000), "MMM d");
  const formattedSunrise = format(new Date(data.sunrise * 1000), "h:mm a");
  const formattedSunset = format(new Date(data.sunset * 1000), "h:mm a");

  const visibilityInKm = (data.visibility || 0) / 1000;
  const windSpeed = data.wind_speed || 0;

  return (
    <div className={styles.card}>
      <div className={styles.topSection} style={{ backgroundColor }}>
        <div className={styles.mainInfo}>
          <span className={styles.location}>
            {data.name}, {data.country}
          </span>
          <span className={styles.dateTime}>
            {formattedTime}, {formattedDate}
          </span>
          <div
            style={{
              marginTop: "1rem",
              display: "flex",
              alignItems: "center",
              gap: "0.5rem",
            }}
          >
            <img src={getWeatherIcon(data.icon)} alt={data.description} />
            <span>{data.description}</span>
          </div>
        </div>
        <div className={styles.weatherInfo}>
          <span className={styles.temp}>
            {Math.round(data.temp)}
            <sup>°C</sup>
          </span>
          <span className={styles.tempRange}>
            Temp Min: {Math.round(data.temp_min)}°C
            <br />
            Temp Max: {Math.round(data.temp_max)}°C
          </span>
        </div>
      </div>

      <div className={styles.bottomSection}>
        <div className={styles.detailItem}>
          Pressure: <strong>{data.pressure}hPa</strong>
          <br />
          Humidity: <strong>{data.humidity}%</strong>
          <br />
          Visibility: <strong>{visibilityInKm.toFixed(1)}km</strong>
        </div>
        <div className={styles.wind}>
          <WiDirectionUp
            style={{ transform: `rotate(${data.wind_deg || 0}deg)` }}
          />
          {/* Use the safe variable here */}
          {windSpeed.toFixed(1)}m/s {data.wind_deg || 0} Degree
        </div>
        <div className={styles.detailItem} style={{ textAlign: "right" }}>
          Sunrise: <strong>{formattedSunrise}</strong>
          <br />
          Sunset: <strong>{formattedSunset}</strong>
        </div>
      </div>
    </div>
  );
};

export default WeatherCard;
