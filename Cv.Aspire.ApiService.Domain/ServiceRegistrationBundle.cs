using Microsoft.Extensions.DependencyInjection;

namespace Cv.Aspire.ApiService.Domain;

/// <summary>
/// Contains generic registration for the Domain layer
/// </summary>
public class ServiceRegistrationBundle : IServiceRegistrationBundle
{
    public void Register(IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}