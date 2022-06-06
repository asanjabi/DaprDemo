using Microsoft.EntityFrameworkCore;

namespace WeatherForecast.ForecastData;

public class WeatherForecastDbContext : DbContext
{
    public WeatherForecastDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<ForecastRecord> Forecasts => Set<ForecastRecord>();
}
