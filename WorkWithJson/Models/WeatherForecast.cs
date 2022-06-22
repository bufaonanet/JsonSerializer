namespace WorkWithJson.Models;

public class WeatherForecast
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public Coordinates? Coordinates { get; set; }
    public Wind? Wind { get; set; }
    public string[]? SummaryWords { get; set; }
}

