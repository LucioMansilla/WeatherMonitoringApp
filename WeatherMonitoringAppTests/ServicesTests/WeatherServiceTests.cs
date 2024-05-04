using AutoFixture;
using Moq;
using WeatherMonitoringApp.Bots;
using WeatherMonitoringApp.Models;
using WeatherMonitoringApp.Services;

namespace WeatherMonitoringAppTests.ServicesTests;

public class WeatherServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IBot> _mockBot1;
    private readonly Mock<IBot> _mockBot2;
    private readonly WeatherService _weatherService;

    public WeatherServiceTests()
    {
        _weatherService = new WeatherService();

        _mockBot1 = new Mock<IBot>();

        _mockBot2 = new Mock<IBot>();

        _fixture = new Fixture();
    }

    [Fact]
    public void UpdateWeather_ShouldCallUpdateOnSubscribedBots()
    {
        // Arrange
        var weatherData = _fixture.Create<WeatherData>();
        _weatherService.Subscribe(_mockBot1.Object);
        _weatherService.Subscribe(_mockBot2.Object);

        // Act
        _weatherService.UpdateWeather(weatherData);

        // Assert
        _mockBot1.Verify(bot => bot.Update(weatherData), Times.Once);
        _mockBot2.Verify(bot => bot.Update(weatherData), Times.Once);
    }

    [Fact]
    public void UpdateWeather_ShouldNotCallUpdateOnUnsubscribedBots()
    {
        // Arrange
        var weatherData = _fixture.Create<WeatherData>();
        _weatherService.Subscribe(_mockBot1.Object);

        // Act
        _weatherService.UpdateWeather(weatherData);

        // Assert
        _mockBot1.Verify(bot => bot.Update(weatherData), Times.Once);
        _mockBot2.Verify(bot => bot.Update(weatherData), Times.Never);
    }
}