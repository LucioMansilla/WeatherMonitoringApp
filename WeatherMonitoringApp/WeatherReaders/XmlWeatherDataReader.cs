using System.Xml.Serialization;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringApp.WeatherReaders;

public class XmlWeatherDataReader : IWeatherDataReader
{
    public WeatherData ReadWeatherData(string xml)
    {
        var serializer = new XmlSerializer(typeof(WeatherData));
        using var reader = new StringReader(xml);
        return (WeatherData)serializer.Deserialize(reader);
    }
}