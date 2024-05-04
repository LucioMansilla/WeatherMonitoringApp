using FluentAssertions;
using WeatherMonitoringApp.WeatherReaders;

namespace WeatherMonitoringAppTests.WeatherReaderTests;

public class XmlWeatherDataReaderTests
{
    [Fact]
    public void ReadWeatherData_ShouldReturnCorrectWeatherData_WhenXmlIsValid()
    {
        // Arrange
        var xmlReader = new XmlWeatherDataReader();
        var validXml = "<WeatherData><Temperature>25</Temperature><Humidity>75</Humidity></WeatherData>";

        // Act
        var result = xmlReader.ReadWeatherData(validXml);

        // Assert
        result.Temperature.Should().Be(25);
        result.Humidity.Should().Be(75);
    }

    [Fact]
    public void ReadWeatherData_ShouldThrowInvalidOperationException_WhenXmlIsInvalid()
    {
        // Arrange
        var xmlReader = new XmlWeatherDataReader();
        var invalidXml = "<bad xml>";

        // Act
        var act = () => xmlReader.ReadWeatherData(invalidXml);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}