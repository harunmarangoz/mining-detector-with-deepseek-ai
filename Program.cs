using Microsoft.OpenApi.Models;
using MiningDetector.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mining Detector API",
        Version = "v1",
        Description = "An API that analyzes log files for cryptocurrency mining behavior.",
        Contact = new OpenApiContact
        {
            Name = "API Support",
            Email = "support@miningdetector.com"
        }
    });
    c.EnableAnnotations();
});

builder.Services.AddScoped<CryptoMinerAiAnalyzer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mining Detector API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();