using Cv.Aspire.Web.Code.Clients.Api;
using Cv.Aspire.Web.Code.Clients.Api.Models;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Cv.Aspire.Web.Code.SemanticKernel.Plugins;

public class WeatherPlugin(ApiClient apiClient)
{
    [KernelFunction]
    [Description("Get the current weather for a specific location")]
    [return: Description("A model containing the current weather information and the units")]
    public async Task<CurrentWeatherForecast?> SetThemeColors(string location)
    {
        return await apiClient.GetCurrentWeatherForLocationAsync(location);
    }
}