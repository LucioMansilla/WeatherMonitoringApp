namespace WeatherMonitoringApp.Models;

public class BotConfigurations
{
    public BotSettings RainBot { get; set; }
    public BotSettings SunBot { get; set; }
    public BotSettings SnowBot { get; set; }
}

public class BotSettings
{
    public bool Enabled { get; set; }
    public int Threshold { get; set; }
    public string Message { get; set; }
}