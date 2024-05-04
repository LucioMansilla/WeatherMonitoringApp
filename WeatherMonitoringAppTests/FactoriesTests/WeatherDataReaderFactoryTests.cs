using FluentAssertions;
using Moq;
using WeatherMonitoringApp.Factories;
using WeatherMonitoringApp.WeatherReaders;

namespace WeatherMonitoringAppTests.FactoriesTests;

public class WeatherDataReaderFactoryTests
{
    private readonly WeatherDataReaderFactory _factory;
    private readonly Mock<IServiceProvider> _mockServiceProvider;

    public WeatherDataReaderFactoryTests()
    {
        _mockServiceProvider = new Mock<IServiceProvider>();
        _factory = new WeatherDataReaderFactory(_mockServiceProvider.Object);
    }

    [Fact]
    public void CreateWeatherDataReader_ShouldReturnJsonWeatherDataReader_WhenInputIsJson()
    {
        // Arrange
        var jsonReader = new JsonWeatherDataReader();
        _mockServiceProvider.Setup(sp => sp.GetService(typeof(JsonWeatherDataReader))).Returns(jsonReader);

        // Act
        var result = _factory.CreateWeatherDataReader("{ 'Temperature': 25, 'Humidity': 70 }");

        // Assert
        result.Should().BeOfType<JsonWeatherDataReader>();
    }

    [Fact]
    public void CreateWeatherDataReader_ShouldReturnXmlWeatherDataReader_WhenInputIsXml()
    {
        // Arrange
        var xmlReader = new XmlWeatherDataReader();
        _mockServiceProvider.Setup(sp => sp.GetService(typeof(XmlWeatherDataReader))).Returns(xmlReader);

        // Act
        var result = _factory.CreateWeatherDataReader("<WeatherData><Temperature>25</Temperature></WeatherData>");

        // Assert
        result.Should().BeOfType<XmlWeatherDataReader>();
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("{ 'unrecognized': 'data' }")]
    [InlineData("<html></html>")]
    public void CreateWeatherDataReader_ShouldReturnNull_WhenInputIsInvalid(string input)
    {
        // Act
        var result = _factory.CreateWeatherDataReader(input);

        // Assert
        result.Should().BeNull();
    }
}