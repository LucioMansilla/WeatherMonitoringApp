using FluentAssertions;
using Newtonsoft.Json;
using WeatherMonitoringApp.WeatherReaders;

namespace WeatherMonitoringAppTests.WeatherReaderTests;

public class JsonWeatherDataReaderTests
{
    [Fact]
    public void ReadWeatherData_ShouldReturnCorrectWeatherData_WhenJsonIsValid()
    {
        // Arrange
        var jsonReader = new JsonWeatherDataReader();
        var validJson = "{\"Temperature\": 25, \"Humidity\": 75}";

        // Act
        var result = jsonReader.ReadWeatherData(validJson);

        // Assert
        result.Temperature.Should().Be(25);
        result.Humidity.Should().Be(75);
    }

    [Fact]
    public void ReadWeatherData_ShouldThrowJsonException_WhenJsonIsInvalid()
    {
        // Arrange
        var jsonReader = new JsonWeatherDataReader();
        var invalidJson = "{bad json}";

        // Act
        var act = () => jsonReader.ReadWeatherData(invalidJson);

        // Assert
        act.Should().Throw<JsonReaderException>();
    }
}