using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.WeatherReaders;

public interface IWeatherDataReader
{
    WeatherData ReadWeatherData(string weatherPlainData);
}