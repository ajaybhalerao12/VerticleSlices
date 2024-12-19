using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newsletter.Api.Extensions;
using Newsletter.Api.Features.Articles;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services
    .AddDbContext(builder.Configuration)
    .AddNSwagServices()
    .AddHealthChecks()
    .AddPostgreSQLHealthCheck(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.RegisterNSwagMiddleware();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
CreateArticleEndpoint.AddRoutes(app);
GetArticleEndpoint.AddRoutes(app);

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
