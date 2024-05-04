using AutoFixture.Xunit2;
using FluentAssertions;
using WeatherMonitoringApp.Bots;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringAppTests.BotTests;

public class SunBotTests : BotTestsBase
{
    private readonly SunBot _sunBot;

    public SunBotTests()
    {
        InitializeBotSettings(30, "Wow, it's a scorcher out there!", c => c.SunBot = _botSettings);
        _sunBot = new SunBot(_mockOptions.Object);
    }

    [Theory]
    [InlineAutoData(35)]
    [InlineAutoData(40)]
    public void Update_ShouldPrintMessage_WhenTemperatureExceedsThreshold(int temperature)
    {
        // Arrange
        var weatherData = new WeatherData { Temperature = temperature };

        // Act
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _sunBot.Update(weatherData);

        // Assert
        var output = writer.GetStringBuilder().ToString().Trim();
        output.Should().Be("SunBot activated!\nSunBot: \"Wow, it's a scorcher out there!\"");
    }

    [Theory]
    [InlineAutoData(25)]
    [InlineAutoData(20)]
    public void Update_ShouldNotPrintMessage_WhenTemperatureIsBelowThreshold(int temperature)
    {
        // Arrange
        var weatherData = new WeatherData { Temperature = temperature };

        // Act
        using var writer = new StringWriter();
        Console.SetOut(writer);
        _sunBot.Update(weatherData);

        // Assert
        var output = writer.GetStringBuilder().ToString().Trim();
        output.Should().BeEmpty();
    }
}