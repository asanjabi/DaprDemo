using Microsoft.AspNetCore.Mvc;

using WeatherForecast.ForecastData;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace WeatherForecast.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly WeatherForecastDbContext _context;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherForecastDbContext context)
    {
        this._logger = logger;
        this._context = context;
    }
   
    private static readonly string[] _azureSqlScopes = new[]
    {
        "https://database.windows.net/.default"
        //"https://vault.azure.net/.default"
    };

    [HttpGet(Name = "GetWeatherForecast/")]
    public async Task<IEnumerable<ForecastRecord>> Get([FromQuery] DateTime startDate)
    {
         try
        {
            var res = await this._context.Database.EnsureCreatedAsync();

            if (this._context.Forecasts.Select(r => r.Id).Count() == 0)
            {
                await this.CreateRandomValues(startDate);
            }

            var result = this._context.Forecasts.OrderByDescending(rec => rec.Date).Take(5).ToArray();
            return result;
        }catch(Exception ex)
        {
            throw;
        }

    }

    private async Task CreateRandomValues(DateTime startDate)
    {
        if (startDate == DateTime.MinValue)
        {
            startDate = DateTime.Now;
        }

        var values = Enumerable.Range(1, 50).Select(index => new ForecastRecord
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        await this._context.Forecasts.AddRangeAsync(values);
        this._context.SaveChanges();
    }
}
