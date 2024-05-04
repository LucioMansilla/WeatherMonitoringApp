using AutoFixture.Xunit2;
using FluentAssertions;
using WeatherMonitoringApp.Bots;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringAppTests.BotTests;

public class SnowBotTests : BotTestsBase
{
    private readonly SnowBot _snowBot;

    public SnowBotTests()
    {
        InitializeBotSettings(0, "Brrr, it's getting chilly!", c => c.SnowBot = _botSettings);
        _snowBot = new SnowBot(_mockOptions.Object);
    }

    [Theory]
    [InlineAutoData(-5)]
    [InlineAutoData(-10)]
    public void Update_ShouldPrintMessage_WhenTemperatureIsBelowThreshold(int temperature)
    {
        // Arrange
        var weatherData = new WeatherData { Temperature = temperature };

        // Act
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _snowBot.Update(weatherData);

        // Assert
        var output = writer.GetStringBuilder().ToString().Trim();
        output.Should().Be("SnowBot activated!\nSnowBot: \"Brrr, it's getting chilly!\"");
    }

    [Theory]
    [InlineAutoData(5)]
    [InlineAutoData(10)]
    public void Update_ShouldNotPrintMessage_WhenTemperatureIsAboveThreshold(int temperature)
    {
        // Arrange
        var weatherData = new WeatherData { Temperature = temperature };

        // Act
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _snowBot.Update(weatherData);

        // Assert
        var output = writer.GetStringBuilder().ToString().Trim();
        output.Should().BeEmpty();
    }
}