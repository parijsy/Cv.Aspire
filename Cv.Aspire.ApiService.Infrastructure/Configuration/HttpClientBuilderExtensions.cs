using Microsoft.Extensions.DependencyInjection;

namespace Cv.Aspire.ApiService.Infrastructure;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder WithBaseAddress(this IHttpClientBuilder builder, Uri? baseAddress) =>
        builder.ConfigureHttpClient(x => x.BaseAddress = baseAddress);
}