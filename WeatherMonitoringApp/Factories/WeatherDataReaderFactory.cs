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

    public IWeatherDataReader? CreateWeatherDataReader(string userInput)
    {
        var format = DetermineDataFormat(userInput);

        return format switch
        {
            "json" => _serviceProvider.GetService<JsonWeatherDataReader>(),
            "xml" => _serviceProvider.GetService<XmlWeatherDataReader>(),
            _ => null
        };
    }

    private string DetermineDataFormat(string userInput)
    {
        if (string.IsNullOrWhiteSpace(userInput))
            return "invalid";

        userInput = userInput.Trim();

        if (userInput.StartsWith("{"))
            return "json";
        return userInput.StartsWith("<") ? "xml" : "invalid";

    }
}