using System.Text.Json.Serialization;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast.Models;

public record CurrentUnits
{
    public required string Time { get; set; }
    public required string Interval { get; set; }

    [JsonPropertyName("temperature_2m")]
    public required string Temperature2m { get; set; }
    public required string Rain { get; set; }
    [JsonPropertyName("weather_code")]
    public required string WeatherCode { get; set; }
}