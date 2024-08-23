
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cv.Aspire.ApiService.Infrastructure;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Finds all implementation of <see cref="IServiceRegistrationBundle"/> within an assembly and calls the Register methods of those implementations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterBundlesInAssembly<T>(this IServiceCollection services, IConfiguration configuration)
    {
        var types = GetImplementationTypes<T>();
        foreach (var type in types)
        {
            if (Activator.CreateInstance(type) is IServiceRegistrationBundle bundle)
            {
                bundle.Register(services);
                bundle.Register(services, configuration);
            }
        }

        return services;
    }

    private static Type[] GetImplementationTypes<T>() => typeof(T).Assembly
        .GetTypes()
        .Where(x => typeof(IServiceRegistrationBundle).IsAssignableFrom(x))
        .Where(x => !x.IsAbstract)
        .Where(x => !x.IsGenericTypeDefinition)
        .Where(x => !x.IsInterface)
        .ToArray();
}
