namespace Website.WeatherService;
public class WeatherForecastClient : IWeatherForecastClient
{
    private readonly HttpClient _httpClient;

    public WeatherForecastClient(HttpClient httpClient)
    {
        this._httpClient = httpClient;
    }

    public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        WeatherForecast[]? forecast = await this._httpClient.GetFromJsonAsync<WeatherForecast[]>($"WeatherForecast?startDate={startDate.ToShortDateString()}");

        return forecast ?? new WeatherForecast[0];
    }
}
