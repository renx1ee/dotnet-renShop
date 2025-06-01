using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RenStore.Application.BackgroundServices;

public class ExampleHostedService(ILogger<ExampleHostedService> logger) : IHostedService
{
    public Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Started ...");

        while (!stoppingToken.IsCancellationRequested)
        {
            OnUpdate(stoppingToken);
        }

        return Task.CompletedTask;
    }

    public async Task OnUpdate(CancellationToken stoppingToken)
    {
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopped ...");
        
        return Task.CompletedTask;
    }
}