namespace Website.WeatherService;

public class WeatherService : IWeatherService
{
    private readonly IWeatherForecastClient _serviceClient;

    public WeatherService(IWeatherForecastClient serviceClient)
    {
        this._serviceClient = serviceClient;
    }

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate = default)
    {
        if (startDate == default)
        {
            startDate = DateTime.Now;
        }

        Task<WeatherForecast[]>? result = this._serviceClient.GetForecastAsync(startDate);
        return result;
    }
}
