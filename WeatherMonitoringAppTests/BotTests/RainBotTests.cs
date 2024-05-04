using AutoFixture.Xunit2;
using FluentAssertions;
using WeatherMonitoringApp.Bots;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringAppTests.BotTests;

public class RainBotTests : BotTestsBase
{
    private readonly RainBot _rainBot;

    public RainBotTests()
    {
        InitializeBotSettings(70, "It looks like it's about to pour down!", c => c.RainBot = _botSettings);
        _rainBot = new RainBot(_mockOptions.Object);
    }

    [Theory]
    [InlineAutoData(75)]
    [InlineAutoData(80)]
    public void Update_ShouldPrintMessage_WhenHumidityExceedsThreshold(int humidity)
    {
        // Arrange
        var weatherData = new WeatherData { Humidity = humidity };

        // Act
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _rainBot.Update(weatherData);

        // Assert
        var output = writer.GetStringBuilder().ToString().Trim();
        output.Should().Be("RainBot activated!\nRainBot: \"It looks like it's about to pour down!\"");
    }

    [Theory]
    [InlineAutoData(65)]
    [InlineAutoData(60)]
    public void Update_ShouldNotPrintMessage_WhenHumidityIsBelowThreshold(int humidity)
    {
        // Arrange
        var weatherData = new WeatherData { Humidity = humidity };

        // Act
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _rainBot.Update(weatherData);

        // Assert
        var output = writer.GetStringBuilder().ToString().Trim();
        output.Should().BeEmpty();
    }
}