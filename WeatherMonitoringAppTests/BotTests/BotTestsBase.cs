using Microsoft.Extensions.Options;
using Moq;
using WeatherMonitoringApp.Models;

namespace WeatherMonitoringAppTests.BotTests;

public abstract class BotTestsBase
{
    protected BotSettings _botSettings;
    protected Mock<IOptions<BotConfigurations>> _mockOptions;

    protected void InitializeBotSettings(int threshold, string message, Action<BotConfigurations> configureBot)
    {
        _botSettings = new BotSettings
        {
            Threshold = threshold,
            Message = message,
            Enabled = true
        };

        var botConfigurations = new BotConfigurations();
        configureBot(botConfigurations);

        _mockOptions = new Mock<IOptions<BotConfigurations>>();
        _mockOptions.Setup(o => o.Value).Returns(botConfigurations);
    }
}