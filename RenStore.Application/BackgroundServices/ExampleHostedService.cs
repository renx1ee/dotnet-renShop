using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RenStore.Application.BackgroundServices;

public class ExampleHostedService(ILogger<ExampleHostedService> logger) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Started ...");
        
        // Если здесь будет бесконечный цикл, то приложение не запустится

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopped ...");
        
        return Task.CompletedTask;
    }
}