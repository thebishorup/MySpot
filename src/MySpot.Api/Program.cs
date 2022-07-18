using MySpot.Api.Api;
using MySpot.Application;
using MySpot.Core;
using MySpot.Infrastructure;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Register logging middleware
builder.Host.UseSerilog(((context, configuration) =>
{
    configuration.WriteTo
        .Console()
        .WriteTo
        .File("logs.txt");
}));

var app = builder.Build();
app.UseInfrastructure();

// Minimal API
app.UseUsersApi();

app.Run();