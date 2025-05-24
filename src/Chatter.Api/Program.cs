using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

// builder.Services
//     .AddApplication()
//     .AddInfrastructure()
//     .AddApi;

var app = builder.Build();

await app.RunAsync();
