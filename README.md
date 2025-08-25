# Weather-Dashboard

Full Stack Weather Application

✨ Objective

The primary objective is to develop a secure web/API application that retrieves and displays weather information, integrating authentication and authorization mechanisms using Auth0.

<img width="1856" height="865" alt="Screenshot 2025-08-25 000713" src="https://github.com/user-attachments/assets/0aaa463c-9bb1-4a1b-823b-1578bb50d20b" />

🚀 Features

Part 1: Weather Information Web/API

[✔] Backend API developed with ASP.NET Core Web API.

[✔] Reads a list of city codes from a local cities.json file.

[✔] Fetches real-time weather data from the OpenWeatherMap API.

[✔] Implements server-side caching with a 5-minute expiration to reduce redundant API calls.

[✔] Frontend UI built with React.js to display weather information.

[✔] A fully responsive UI that adapts to both desktop and mobile resolutions.

Part 2: Authentication & Authorization

[✔] Secure user authentication implemented using Auth0.

[✔] Protected API endpoints that require a valid JWT Bearer token for access.

[✔] Users must log in to view the weather data.

[✔] Multi-Factor Authentication (MFA) is enabled via Email Verification for enhanced security.

[✔] Public user signups are disabled; access is restricted to pre-registered users.


🔧 Technology Stack

Backend

Framework: ASP.NET Core 8.0 Web API

Language: C#

Authentication: Microsoft JWT Bearer Authentication for .NET

API Client: IHttpClientFactory

Caching: IMemoryCache

Frontend

Library: React.js

State Management: React Hooks (useState, useEffect)

API Client: Axios

Authentication: Auth0 React SDK 

Styling: CSS Modules & Flexbox/Grid for responsiveness

Icons: react-icons



