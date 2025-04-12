using Infrastructure;
using Presentation.Endpoint;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

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
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Vialmentor API Bridge", Version = "v1" });
});

// Inject the configuration into the DI container and register the services
services.AddInfrastructure(configuration);
services.AddUseCases();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapAppEndpoints();
app.MapSystemEndpoints();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "docs";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vialmentor API Bridge");
});

app.Run();