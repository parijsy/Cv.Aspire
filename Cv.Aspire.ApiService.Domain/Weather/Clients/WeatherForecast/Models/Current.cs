using System.Text.Json.Serialization;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast.Models;

public record Current
{
    public DateTime Time { get; set; }
    public int Interval { get; set; }

    [JsonPropertyName("temperature_2m")]
    public float Temperature2m { get; set; }
    public float Rain { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }
}