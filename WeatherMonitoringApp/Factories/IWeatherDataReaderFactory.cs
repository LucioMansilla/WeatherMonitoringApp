using WeatherMonitoringApp.WeatherReaders;

namespace WeatherMonitoringApp.Factories;

public interface IWeatherDataReaderFactory
{
    IWeatherDataReader? CreateWeatherDataReader(string format);
}