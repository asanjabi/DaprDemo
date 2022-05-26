namespace Website.WeatherService;

/// <summary>
/// This internface and implementing class encapsulates all business logic 
/// related to weather service from this application's prespective
/// </summary>
public interface IWeatherService
{
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate = default);
}