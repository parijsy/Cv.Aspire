using System.Text.Json.Serialization;

namespace Cv.Aspire.Web.Code.Clients.Api.Models;

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