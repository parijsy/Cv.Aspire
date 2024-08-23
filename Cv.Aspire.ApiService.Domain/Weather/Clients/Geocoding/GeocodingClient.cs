using Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding.Models;
using System.Net.Http.Json;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding;

/// <summary>
/// For documentation see https://open-meteo.com/en/docs/geocoding-api
/// </summary>
/// <param name="httpClient"></param>
internal class GeocodingClient(HttpClient httpClient) : IGeocodingClient
{
    public async Task<Result<GeocodingLocationModel[]>> SearchAsync(string query, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.GetAsync($"/v1/search?name={query}&count=10&language=en&format=json", cancellationToken);
            if (!response.IsSuccessStatusCode)
                return new Error("Geocoding request failed");

            var result = await response.Content.ReadFromJsonAsync<GeocodingResponseModel>(cancellationToken);
            if (result == null)
                return new Error("Unable to read Geocoding response");

            return Result.Ok(result.Results);
        }
        catch (Exception ex)
        {
            return new Error($"Exception occured in {nameof(GeocodingClient)}.{nameof(SearchAsync)}")
                .CausedBy(ex);
        }
    }
}