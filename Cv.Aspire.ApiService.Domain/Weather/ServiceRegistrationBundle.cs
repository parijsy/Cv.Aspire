using Cv.Aspire.ApiService.Domain.Weather.Clients.Geocoding;
using Cv.Aspire.ApiService.Domain.Weather.Clients.WeatherForecast;
using Cv.Aspire.ApiService.Domain.Weather.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cv.Aspire.ApiService.Domain.Weather;

/// <summary>
/// Contains all registrations for the WeatherService
/// </summary>
public class ServiceRegistrationBundle : IServiceRegistrationBundle
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IWeatherService, WeatherService>();
        services.AddHttpClient<IGeocodingClient, GeocodingClient>().WithBaseAddress(configuration, "BaseAddress:GeoCoding");
        services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>().WithBaseAddress(configuration, "BaseAddress:WeatherForecast");
    }
}