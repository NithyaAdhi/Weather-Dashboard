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

⚙️ Getting Started
Follow these instructions to set up and run the project locally.
Prerequisites
.NET 8.0 SDK
Node.js: (v16 or later)
Visual Studio 2022: For running the backend API.
VS Code (or any text editor): For running the frontend React app.
1. Configuration & Environment Variables
Before running the applications, you need to configure your secret keys and identifiers.
Backend Configuration (backend/appsettings.json)
Update the appsettings.json file in the ASP.NET Core project with your keys:
code
JSON
{
  // ... other settings
  "OpenWeather": {
    "ApiKey": "[YOUR_OPENWEATHERMAP_API_KEY]"
  },
  "Auth0": {
    "Domain": "https://[YOUR_AUTH0_DOMAIN]",
    "Audience": "[YOUR_AUTH0_API_IDENTIFIER]"
  }
}
Frontend Configuration (frontend/src/index.js)
Update the Auth0Provider component in frontend/src/index.js with your Auth0 details:
code
JavaScript
// src/index.js
root.render(
  <React.StrictMode>
    <Auth0Provider
      domain="[YOUR_AUTH0_DOMAIN]"
      clientId="[YOUR_AUTH0_CLIENT_ID]"
      authorizationParams={{
        redirect_uri: window.location.origin,
        audience: "[YOUR_AUTH0_API_IDENTIFIER]"
      }}
    >
      <App />
    </Auth0Provider>
  </React.StrictMode>
);

2. Running the Backend API
Clone the repository:
code
Bash
git clone [your-repository-url]
cd [your-repository-name]
Open the Backend solution (.sln file) in Visual Studio 2022.
Restore Dependencies: The dependencies should restore automatically. If not, build the project (Ctrl+Shift+B).
Run the project: Press F5 or the green play button to start the API. The server will launch on a specific port (e.g., https://localhost:7208).
3. Running the Frontend React App
Open a new terminal and navigate to the frontend directory:
code
Bash
cd frontend
Install dependencies:
code
Bash
npm install
Start the development server:
code
Bash
npm start
The application will open in your browser at http://localhost:3000.
You should now be able to log in and view the weather application!
📂 Project Structure
The project is organized into two main folders, backend and frontend, to maintain a clear separation of concerns.
code
Code
.
├── backend/                  # ASP.NET Core Web API Project
│   ├── Controllers/          # API controllers (WeatherController.cs)
│   ├── Models/               # C# data models (WeatherResponse.cs, etc.)
│   ├── Services/             # Business logic (WeatherService.cs)
│   ├── appsettings.json      # Configuration and secrets
│   └── ...
├── frontend/                 # React.js Project
│   ├── public/
│   └── src/
│       ├── components/       # Reusable React components
│       ├── services/         # API call logic (weatherService.js)
│       ├── App.js            # Main application component
│       └── index.js          # Entry point and Auth0 provider setup
└── README.md                 # This file
