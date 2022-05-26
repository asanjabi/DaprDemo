namespace Website.WeatherService;

/// <summary>
/// This interface and implementing class encapsulates
/// all the call invocations to weather forecast service
/// </summary>
public interface IWeatherForecastClient
{
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
}