using System.Text.Json.Serialization;

namespace Cv.Aspire.Web.Code.Clients.Api.Models;

public record WeatherForecastResponseModel
{
    [JsonPropertyName("generationtime_ms")]
    public float GenerationTimeInMs { get; set; }

    [JsonPropertyName("current_units")]
    public required CurrentUnits CurrentUnits { get; set; }

    public required Current Current { get; set; }
}