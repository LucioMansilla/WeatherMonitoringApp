using WeatherMonitoringApp.Bots;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.Services;

public class WeatherService
{
    private readonly List<IBot> _subscribedBots;

    public WeatherService()
    {
        _subscribedBots = new List<IBot>();
    }

    public void Subscribe(IBot bot)
    {
        _subscribedBots.Add(bot);
    }

    public void UpdateWeather(WeatherData weatherData)
    {
        _subscribedBots.ForEach(bot => bot.Update(weatherData));
    }
}