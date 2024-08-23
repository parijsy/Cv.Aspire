using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cv.Aspire.ApiService.Infrastructure;

/// <summary>
/// This interface can be used to create a bundle of registrations.
/// </summary>
public interface IServiceRegistrationBundle
{
    public void Register(IServiceCollection services) { }

    public void Register(IServiceCollection services, IConfiguration configuration) { }
}