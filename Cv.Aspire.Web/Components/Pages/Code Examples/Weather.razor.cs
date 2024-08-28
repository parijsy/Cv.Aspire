using Cv.Aspire.Web.Code.Clients.Api;
using Cv.Aspire.Web.Code.Clients.Api.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Cv.Aspire.Web.Components.Pages.Code_Examples;

public partial class Weather : IDisposable
{
    [Inject] private ApiClient ApiClient { get; set; } = null!;
    [Inject] private ILogger<Weather> Logger { get; set; } = null!;

    private string locationQuery = "Amsterdam";
    private CurrentWeatherForecast? forecast;
    private bool isLoading = false;

    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    protected override async Task OnInitializedAsync() =>
        await Submit();

    private async Task Submit()
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource = new CancellationTokenSource();

        try
        {
            isLoading = true;
            forecast = await ApiClient.GetCurrentWeatherForLocationAsync(locationQuery, cancellationTokenSource.Token);
            isLoading = false;
        }
        catch (TaskCanceledException) { }
    }

    private async Task OnKeyUp_Search(KeyboardEventArgs e)
    {
        if (e.Code == "Enter")
            await Submit();
    }

    public string ConvertWmoCode(int code) => code switch
    {
        0 => "Clear Sky",
        1 => "Mainly clear",
        2 => "Partly cloudy",
        3 => "Overcast",
        45 => "Fog",
        48 => "Depositing rime fog",
        51 => "Light drizzle",
        53 => "Moderate drizzle",
        55 => "Dene drizzle",
        56 => "Light freezing drizzle",
        57 => "Dense freezing drizzle",
        61 => "Slight rain",
        63 => "Moderate rain",
        65 => "Heavy rain",
        66 => "Light freezing rain",
        67 => "Heavy freezing rain",
        71 => "Slight snowfall",
        73 => "Moderate snowfall",
        75 => "Heavy snowfall",
        77 => "Snow grains",
        80 => "Slight rain shower",
        81 => "Moderate rain shower",
        82 => "Violent rain shower",
        85 => "Slight snow shower",
        86 => "Heavy snow shower",
        95 => "Thunderstorm",
        99 => "Thunderstorm with hail",
        _ => string.Empty,
    };

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
    }
}