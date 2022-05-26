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
