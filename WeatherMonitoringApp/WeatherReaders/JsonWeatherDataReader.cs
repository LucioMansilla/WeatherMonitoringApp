using Newtonsoft.Json;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.WeatherReaders;

public class JsonWeatherDataReader : IWeatherDataReader
{
    public WeatherData ReadWeatherData(string weatherPlainData)
    {
        return JsonConvert.DeserializeObject<WeatherData>(weatherPlainData);
    }
}