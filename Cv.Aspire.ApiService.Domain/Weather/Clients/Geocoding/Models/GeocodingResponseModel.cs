using System.Text.Json.Serialization;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding.Models;

internal record GeocodingResponseModel
{
    public GeocodingLocationModel[] Results { get; set; } = [];

    [JsonPropertyName("generationtime_ms")]
    public float GenerationTimeInMs { get; set; }
}