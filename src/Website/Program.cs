using AspNetMonsters.ApplicationInsights.AspNetCore;

using Website.WeatherService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.AddSingleton<IWeatherForecastClient, WeatherForecastClient>();

builder.Services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WeatherForecastServiceURL"]);
}).AddHttpMessageHandler(() => new Dapr.Client.InvocationHandler());

//Set the role name to the same value we are using for Dapr app-id so things line up in AppInsights
//You can also use multiple instances of AppInsights with unique name and instrumentation keys
//I'm using a nuget package (AspNetMonsters.ApplicationInsights.AspNetCore) for the telemetry provider, but it is fairly easy to implement one without 
//taking dependency on third party code, look at this blog post.
//https://www.davepaquette.com/archive/2020/02/05/setting-cloud-role-name-in-application-insights.aspx
builder.Services.AddCloudRoleNameInitializer("website");

string? appInsightsConnectionString = builder.Configuration["APPLICATIONINSIGHTS-CONNECTION-STRING"];
if (!string.IsNullOrEmpty(appInsightsConnectionString))
{
    builder.Services.AddApplicationInsightsTelemetry(options => options.ConnectionString = appInsightsConnectionString);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
