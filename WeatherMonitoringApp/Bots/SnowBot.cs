using Microsoft.Extensions.Options;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.Bots;

public class SnowBot : IBot
{
    private readonly BotSettings _settings;

    public SnowBot(IOptions<BotConfigurations> botConfigurations)
    {
        _settings = botConfigurations.Value.SnowBot;
    }

    private static string Name => "SnowBot";
    public void Update(WeatherData weatherData)
    {
        if (weatherData.Temperature < _settings.Threshold)
            Console.WriteLine($"{Name} activated!\n{Name}: \"{_settings.Message}\"");
    }
}