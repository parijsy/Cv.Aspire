using Cv.Aspire.ApiService.Domain.Weather.Interfaces;
using Cv.Aspire.ApiService.Domain.Weather.Models;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Cv.Aspire.ApiService.Controllers;

[ApiController]
[Route("weather")]
public class WeatherController : Controller
{
    [Route("forecast")]
    public async Task<ActionResult<CurrentWeatherForecast?>> ForecastAsync([FromServices] IWeatherService service, string location, CancellationToken cancellationToken) =>
        await service.GetWeatherForecastAsync(location, cancellationToken).ToActionResult();
}