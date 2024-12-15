using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunStartupServices();

app.UseHttpsRedirection();
app.UseHealthChecks("/api/_health");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TenantAuthorizationMiddleware>();

app.MapControllers();

app.Run();