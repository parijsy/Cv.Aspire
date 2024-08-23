using Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast.Models;
using System.Globalization;
using System.Net.Http.Json;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast;

/// <summary>
/// For documentation see https://open-meteo.com/en/docs
/// </summary>
/// <param name="httpClient"></param>
internal class WeatherForecastClient(HttpClient httpClient) : IWeatherForecastClient
{
    public async Task<Result<WeatherForecastResponseModel>> ForecastAsync(float latitude, float longitude, CancellationToken cancellationToken)
    {
        try
        {
            var response = await httpClient.GetAsync($"/v1/forecast?latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}&current=temperature_2m,rain,weather_code", cancellationToken);
            if (!response.IsSuccessStatusCode)
                return new Error("Forecast request failed");

            var result = await response.Content.ReadFromJsonAsync<WeatherForecastResponseModel>(cancellationToken);
            if (result == null)
                return new Error("Unable to read Forecast response");

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            return new Error($"Exception occured in {nameof(WeatherForecastClient)}.{nameof(ForecastAsync)}")
                .CausedBy(ex);
        }
    }
}