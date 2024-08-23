using Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast.Models;

namespace Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast;

public interface IWeatherForecastClient
{
    Task<Result<WeatherForecastResponseModel>> ForecastAsync(float latitude, float longitude, CancellationToken cancellationToken);
}