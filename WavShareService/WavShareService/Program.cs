using Microsoft.Extensions.Diagnostics.HealthChecks;
using WavShareService.Extensions;
using WavShareService.HealthCheck;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddCheck(
        "WavShareServiceDb-Check", 
        new WavShareDbHealthCheck(builder.Configuration.GetConnectionString("WavShareDbFake")),
        HealthStatus.Unhealthy,
        new string[] { "wavshareservicedb" }
        );
builder.Services.AddConfiguredLogging(builder.Configuration);
builder.Services.AddConfiguredRouting();
builder.Services.AddConfiguredControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguredSwaggerGen();
builder.Services.AddTransientServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultResponseHeaderMiddleware();

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
