using Microsoft.Extensions.Options;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.Bots;
public class SunBot : IBot
{
    private readonly BotSettings _settings;

    public SunBot(IOptions<BotConfigurations> botConfigurations)
    {
        _settings = botConfigurations.Value.SunBot;
    }

    private static string Name => "SunBot";

    public void Update(WeatherData weatherData)
    {
        if (weatherData.Temperature > _settings.Threshold)
            Console.WriteLine($"{Name} activated!\n{Name}: \"{_settings.Message}\"");
    }
}