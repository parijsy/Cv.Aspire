using Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding;
using Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast;
using Cv.Aspire.ApiService.Domain.Weather.Interfaces;
using Cv.Aspire.ApiService.Domain.Weather.Models;

namespace Cv.Aspire.ApiService.Domain.Weather;

internal class WeatherService(IGeocodingClient geoCodingClient, IWeatherForecastClient weatherForecastClient) : IWeatherService
{
    public async Task<Result<CurrentWeatherForecast?>> GetWeatherForecastAsync(string locationQuery, CancellationToken cancellationToken)
    {
        var locations = await geoCodingClient.SearchAsync(locationQuery, cancellationToken);
        if (locations.IsFailed)
            return new Error("Unable to retrieve geocoding locations").CausedBy(locations.Errors);

        if (locations.Value.Length == 0)
            return new Result<CurrentWeatherForecast?>();

        var location = locations.Value.First();
        var forecastResult = await weatherForecastClient.ForecastAsync(location.Latitude, location.Longitude, cancellationToken);
        if (forecastResult.IsFailed)
            return new Error("Unable to retrieve forecast").CausedBy(forecastResult.Errors);

        var forecast = forecastResult.Value;
        return new CurrentWeatherForecast(location, forecast);
    }
}