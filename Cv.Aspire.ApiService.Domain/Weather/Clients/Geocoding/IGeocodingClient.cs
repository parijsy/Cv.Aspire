using Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding.Models;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding;

public interface IGeocodingClient
{
    Task<Result<GeocodingLocationModel[]>> SearchAsync(string query, CancellationToken cancellationToken);
}