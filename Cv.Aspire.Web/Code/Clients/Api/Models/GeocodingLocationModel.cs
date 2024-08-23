using System.Text.Json.Serialization;

namespace Cv.Aspire.Web.Code.Clients.Api.Models;

public record GeocodingLocationModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public float Elevation { get; set; }
    public required string Timezone { get; set; }

    [JsonPropertyName("country_code")]
    public required string CountryCode { get; set; }

    public required string Country { get; set; }
}