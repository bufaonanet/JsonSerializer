using System.Text.Json.Serialization;

namespace WorkWithJson.Models;
public class WeatherForecastIgnore
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public Coordinates? Coordinates { get; set; }
    public Wind? Wind { get; set; }
    public string[]? SummaryWords { get; set; }
    public ICollection<WeatherForecast>? Records { get; set; }
    public WeatherCondition WeatherCondition { get; set; }

    // Properties used in a different application, should not be serialized
    [JsonIgnore]
    public string? WeatherLogo { get; set; }
    [JsonIgnore]
    public double RequestTime { get; set; }
    public double TemperatureScore { get; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime DateIgnoreDefault { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int TemperatureIgnoreNever { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SummaryIgnoreNull { get; set; }
}

