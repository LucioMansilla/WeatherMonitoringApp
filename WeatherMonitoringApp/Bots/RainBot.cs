using Microsoft.Extensions.Options;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.Bots;

public class RainBot : IBot
{
    private readonly BotSettings _settings;

    public RainBot(IOptions<BotConfigurations> botConfigurations)
    {
        _settings = botConfigurations.Value.RainBot;
    }

    private static string Name => "RainBot";
    
    public void Update(WeatherData weatherData)
    {
        if (weatherData.Humidity > _settings.Threshold)
            Console.WriteLine($"{Name} activated!\n{Name}: \"{_settings.Message}\"");
    }
}