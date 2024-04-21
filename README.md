# Weather Monitoring Application

This project is a real-time weather monitoring and reporting service developed in C#. The application processes weather data in various formats (JSON, XML, etc.) and activates different types of 'weather bots' based on the weather conditions.

## Table of Contents

- [Project Structure](#project-structure)
- [Dependencies](#dependencies)
- [Compilation and Execution](#compilation-and-execution)
- [Configuration](#configuration)

## Project Structure

The project is organized as follows:

- `Application.cs`: The main and user interface application class.
- `appsettings.json`: Configuration file for the bots.
- `Bots/`: Contains the bots that get activated based on weather conditions.
- `Factories/`: Contains the factory for creating the appropriate weather data reader.
- `Models/`: Contains the models used in the application.
- `Program.cs`: The entry point of the application.
- `Services/`: Contains the service that handles the updating of weather data.
- `Startup.cs`: Configures the services needed by the application.
- `WeatherReaders/`: Contains implementations of `IWeatherDataReader` that read weather data in different formats.

## Dependencies

The following dependencies are required to build and execute the application:

- .NET 5.0 or later (I like .NET 8)
- Newtonsoft.Json for JSON processing

## Compilation and Execution

To build and run the application, use the following commands in the terminal:

```bash
dotnet build
dotnet run
```

## Usage

After starting the application, you will be prompted with a menu in the console where you can choose to enter weather data or exit. Here's how you can interact with the application:

**Enter Weather Data**: You will be prompted to input weather data in a valid format. Based on the data provided, the application will check against the bot configurations in `appsettings.json` and activate the appropriate bots if their conditions are met.

### Example Interaction

#### Json Input

```json
{ "Location": "City Name", "Temperature": 45.0, "Humidity": 40.0 }
```
This input represents the weather data with a temperature of 45.0 degrees Celsius and 40.0% humidity.

#### Expected Output

Given the example configuration in `appsettings.json`, if the `SunBot` is enabled and its temperature threshold is less than 35.0 degrees Celsius, the application will output:

```c#
SunBot activated!
SunBot: "Wow, it's a scorcher out there!"
```

## Configuration

The application's behavior is controlled through the `appsettings.json` file, where you can enable or disable bots, set activation thresholds, and specify messages to be displayed when each bot is activated.

### Example Configuration

The behavior of each bot can be configured in the `appsettings.json` file. Here is an example showing how the bots are configured:

```json
{
  "Bots": {
    "RainBot": {
      "enabled": true,
      "humidityThreshold": 70,
      "message": "It looks like it's about to pour down!"
    },
    "SunBot": {
      "enabled": true,
      "temperatureThreshold": 30,
      "message": "Wow, it's a scorcher out there!"
    },
    "SnowBot": {
      "enabled": false,
      "temperatureThreshold": 0,
      "message": "Brrr, it's getting chilly!"
    }
  }
}
```

This configuration enables the `SunBot` and `RainBot` while disabling the `SnowBot`. The thresholds for temperature and humidity determine when each bot gets activated.