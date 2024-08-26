using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cv.Aspire.ApiService.Infrastructure;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder WithBaseAddress(this IHttpClientBuilder builder, IConfiguration configuration, string key) =>
        builder.ConfigureHttpClient(x => x.BaseAddress = configuration.GetValue<Uri>(key));
}