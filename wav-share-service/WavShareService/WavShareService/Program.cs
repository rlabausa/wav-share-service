using WavShareServiceBLL;
using WavShareServiceDAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLogging();
builder.Services.AddRouting(opts => {
    opts.LowercaseUrls = true;
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.DescribeAllParametersInCamelCase();
});
builder.Services.AddTransient<IAudioFileBLL, AudioFileBLL>();
builder.Services.AddTransient<IAudioFileAdapter, AudioFileAdapter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
