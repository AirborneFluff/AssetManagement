using API.Services.Startup;

namespace API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task RunStartupServices(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var startupServices = scope.ServiceProvider.GetServices<IStartupService>();

        foreach (var service in startupServices)
        {
            var logger = scope.ServiceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger(service.GetType());

            var result = await service.Execute();
            if (result.IsSuccess)
            {
                logger.LogInformation($"{service.GetType().Name} executed successfully.");
                continue;
            }

            result.Errors.ForEach(e => logger.LogWarning($"{service.GetType().Name} encountered an error: {e.Message}"));
        }
    }
}