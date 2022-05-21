namespace Website.Data;

public class WeatherForecastService
{
    private readonly WeatherServiceClient _serviceClient;

    public WeatherForecastService(WeatherServiceClient serviceClient)
    {
        this._serviceClient = serviceClient;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        var result = this._serviceClient.GetForecastAsync(startDate);
        return result;
    }
}
