using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WavShareService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConfiguredLogging(builder.Configuration);
builder.Services.AddConfiguredRouting();
builder.Services.AddConfiguredControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguredSwaggerGen();
builder.Services.AddTransientServices();
builder.Services.AddConfiguredHealthChecks(builder.Configuration);
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

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

app.MapControllers();

app.MapHealthChecks("/health");

app.MapHealthChecks("/health-ui", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.Run();
