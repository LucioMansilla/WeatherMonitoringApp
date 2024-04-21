using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherMonitoringApp.Factories;
using WeatherMonitoringApp.Services;

namespace WeatherMonitoringApp;

public static class Program
{
    private static IServiceProvider _serviceProvider;
    private static WeatherService _weatherService;
    private static IWeatherDataReaderFactory _weatherDataReaderFactory;

    private static void Main(string[] args)
    {
        SetupServices();

        while (true)
        {
            Console.WriteLine("1. Enter weather data");
            Console.WriteLine("2. Exit");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    EnterWeatherData();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void SetupServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        var serviceCollection = new ServiceCollection();
        var startup = new Startup(configuration);
        startup.ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();

        _weatherService = _serviceProvider.GetRequiredService<WeatherService>();
        _weatherDataReaderFactory = _serviceProvider.GetRequiredService<IWeatherDataReaderFactory>();
    }

    private static void EnterWeatherData()
    {
        Console.WriteLine("Enter weather data:");
        var userInput = Console.ReadLine();

        var weatherDataReader = _weatherDataReaderFactory.CreateWeatherDataReader(userInput);

        if (weatherDataReader == null)
            throw new Exception("Invalid format");

        var weatherData = weatherDataReader.ReadWeatherData(userInput);


        _weatherService.UpdateWeather(weatherData);
    }
}