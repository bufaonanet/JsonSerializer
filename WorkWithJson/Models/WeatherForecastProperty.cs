using System.Text.Json.Serialization;

namespace WorkWithJson.Models;

public class WeatherForecastProperty
{
    public DateTimeOffset Date { get; set; }

    public int TemperatureCelsius { get; set; }

    [JsonPropertyName("description")]
    public string? Summary { get; set; }

    public Feels FeelsLike { get; set; }

    public Dictionary<string, int> TemperatureRanges { get; set; }
}

public enum Feels
{
    Cold, Cool, Warm, Hot
}

