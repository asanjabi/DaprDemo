using AspNetMonsters.ApplicationInsights.AspNetCore;

using Dapr.Client;
using Dapr.Extensions.Configuration;

using Microsoft.EntityFrameworkCore;

using WeatherForecast.ForecastData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var daprClient = new DaprClientBuilder()
    .Build();
builder.Configuration.AddDaprSecretStore(config =>
{
    config.Store = "azurekeyvault";
    config.Client = daprClient;
});

builder.Services.AddDbContext<WeatherForecastDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ForecastDatabaseConnectionString")));

//Set the role name to the same value we are using for Dapr app-id so things line up in AppInsights
//You can also use multiple instances of AppInsights with unique name and instrumentation keys
//I'm using a nuget package (AspNetMonsters.ApplicationInsights.AspNetCore) for the telemetry provider, but it is fairly easy to implement one without 
//tanking dependiency on third party code, look at this blog post.
//https://www.davepaquette.com/archive/2020/02/05/setting-cloud-role-name-in-application-insights.aspx
builder.Services.AddCloudRoleNameInitializer("weatherforecast-svc");

string? appInsightsConnectionString = builder.Configuration["APPLICATIONINSIGHTS-CONNECTION-STRING"];
if (!string.IsNullOrEmpty(appInsightsConnectionString))
{
    builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = appInsightsConnectionString);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
