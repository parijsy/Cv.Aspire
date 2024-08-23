using Cv.Aspire.ApiService.Domain.Weather.Models;

namespace Cv.Aspire.ApiService.Domain.Weather.Interfaces;

public interface IWeatherService
{
    Task<Result<CurrentWeatherForecast?>> GetWeatherForecastAsync(string location, CancellationToken cancellationToken);
}