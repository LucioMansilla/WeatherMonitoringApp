using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.Bots;

public interface IBot
{
    void Update(WeatherData weatherData);
}