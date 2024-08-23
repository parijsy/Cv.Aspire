using Cv.Aspire.Web.Code.Clients.Api.Models;
using System.Net;

namespace Cv.Aspire.Web.Code.Clients.Api;

public class ApiClient(HttpClient httpClient)
{
    public async Task<CurrentWeatherForecast?> GetCurrentWeatherForLocationAsync(string location, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"/weather/forecast?location={location}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Service call failed");

        if (response.StatusCode == HttpStatusCode.NoContent)
            return null;

        return await response.Content.ReadFromJsonAsync<CurrentWeatherForecast?>(cancellationToken);
    }
}