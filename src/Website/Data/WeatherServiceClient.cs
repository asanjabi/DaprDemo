namespace Website.Data;
using Microsoft.AspNetCore.WebUtilities;

public class WeatherServiceClient
{
    private readonly HttpClient _httpClient;

    public WeatherServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        var forecast = await this._httpClient.GetFromJsonAsync<WeatherForecast[]>($"WeatherForecast?startDate={startDate.ToShortDateString()}");

        return forecast ?? new WeatherForecast[0];
    }
}
