using Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding.Models;
using Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast.Models;

namespace Cv.Aspire.ApiService.Domain.Weather.Models;

public record CurrentWeatherForecast(GeocodingLocationModel Location, WeatherForecastResponseModel WeatherForecast) { }