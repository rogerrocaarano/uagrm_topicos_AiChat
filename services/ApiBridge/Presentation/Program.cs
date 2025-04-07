using Infrastructure;
using Presentation.Endpoint;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();

// Inject the configuration into the DI container and register the services
services.AddInfrastructure(configuration);
services.AddUseCases();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapAppEndpoints();
app.MapOpenApi();

app.Run();
