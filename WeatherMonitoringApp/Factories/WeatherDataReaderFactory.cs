using Microsoft.Extensions.DependencyInjection;
using WeatherMonitoringApp.WeatherReaders;

namespace WeatherMonitoringApp.Factories;

public class WeatherDataReaderFactory : IWeatherDataReaderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public WeatherDataReaderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IWeatherDataReader? CreateWeatherDataReader(string format)
    {
        format = format.ToLower();

        return format switch
        {
            "json" => _serviceProvider.GetService<JsonWeatherDataReader>(),
            "xml" => _serviceProvider.GetService<XmlWeatherDataReader>(),
            _ => null
        };
    }
}