using WeatherMonitoringApp.Factories;
using WeatherMonitoringApp.Services;

namespace WeatherMonitoringApp;

public class Application
{
    private readonly IWeatherDataReaderFactory _weatherDataReaderFactory;
    private readonly WeatherService _weatherService;

    public Application(WeatherService weatherService, IWeatherDataReaderFactory weatherDataReaderFactory)
    {
        _weatherService = weatherService;
        _weatherDataReaderFactory = weatherDataReaderFactory;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Enter weather data");
            Console.WriteLine("2. Exit");
            Console.Write("Select an option: ");
            if (Enum.TryParse<MenuOption>(Console.ReadLine(), out var selectedOption) &&
                Enum.IsDefined(typeof(MenuOption), selectedOption))
                HandleMenuOption(selectedOption);
            else
                Console.WriteLine("Invalid option. Please try again.");
        }
    }

    private void HandleMenuOption(MenuOption option)
    {
        switch (option)
        {
            case MenuOption.EnterWeatherData:
                EnterWeatherData();
                break;
            case MenuOption.Exit:
                Environment.Exit(0);
                break;
            default:
                throw new ArgumentException("Invalid menu option");
        }
    }

    private void EnterWeatherData()
    {
        Console.WriteLine("Enter weather data:");
        var userInput = Console.ReadLine();

        var weatherDataReader = _weatherDataReaderFactory.CreateWeatherDataReader(userInput);

        if (weatherDataReader == null)
        {
            Console.WriteLine("Invalid format");
            return;
        }

        var weatherData = weatherDataReader.ReadWeatherData(userInput);
        _weatherService.UpdateWeather(weatherData);
    }

    private enum MenuOption
    {
        EnterWeatherData = 1,
        Exit = 2
    }
}