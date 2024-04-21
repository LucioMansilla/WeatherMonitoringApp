using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WeatherMonitoringApp.Bots;
using WeatherMonitoringApp.Factories;
using WeatherMonitoringApp.Models;
using WeatherMonitoringApp.Services;
using WeatherMonitoringApp.WeatherReaders;

namespace WeatherMonitoringApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<BotConfigurations>(Configuration.GetSection("Bots"));
        services.AddSingleton<IBot, RainBot>();
        services.AddSingleton<IBot, SunBot>();
        services.AddSingleton<IBot, SnowBot>();
        services.AddSingleton<IWeatherDataReaderFactory, WeatherDataReaderFactory>();
        services.AddTransient<JsonWeatherDataReader>();
        services.AddTransient<XmlWeatherDataReader>();

        services.AddSingleton<WeatherService>(serviceProvider =>
        {
            var botConfigurations = serviceProvider.GetRequiredService<IOptions<BotConfigurations>>().Value;

            var bots = serviceProvider.GetServices<IBot>();
            var enabledBots = new List<IBot>();

            if (botConfigurations.RainBot.Enabled)
                enabledBots.Add(bots.OfType<RainBot>().Single());
            
            if (botConfigurations.SunBot.Enabled)
                enabledBots.Add(bots.OfType<SunBot>().Single());

            if (botConfigurations.SnowBot.Enabled)
                enabledBots.Add(bots.OfType<SnowBot>().Single());
            
            var weatherService = new WeatherService();
            enabledBots.ForEach(bot => weatherService.Subscribe(bot));

            return weatherService;
        });
    }
}