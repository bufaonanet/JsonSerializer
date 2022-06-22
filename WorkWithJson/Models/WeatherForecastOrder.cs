using System.Text.Json.Serialization;

namespace WorkWithJson.Models;

public class WeatherForecastOrder
{
    [JsonPropertyOrder(-1)]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyOrder(-2)]
    public int TemperatureCelsius { get; set; }

    [JsonPropertyOrder(-3)]
    public string? Summary { get; set; }

    public int Pressure { get; set; }

    public int Humidity { get; set; }

    [JsonPropertyOrder(2)]
    public Coordinates? Coordinates { get; set; }

    [JsonPropertyOrder(1)]
    public Wind? Wind { get; set; }

    public string[]? SummaryWords { get; set; }

    public ICollection<WeatherForecast>? Records { get; set; }

    public WeatherCondition WeatherCondition { get; set; }
}

public enum WeatherCondition
{
    Snow, Rain, Storm, Huricane
}

