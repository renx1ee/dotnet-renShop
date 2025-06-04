using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RenStore.Application.Interfaces;

namespace RenStore.Application.BackgroundServices;

public class ModerationBackgroundService : BackgroundService
{
    private readonly ILogger<ModerationBackgroundService> logger;
    private readonly IBackgroundTaskQueue taskQueue;
    private readonly IServiceScopeFactory scopeFactory;

    public ModerationBackgroundService(
        ILogger<ModerationBackgroundService> logger,
        IBackgroundTaskQueue taskQueue,
        IServiceScopeFactory scopeFactory)
    {
        this.logger = logger;
        this.taskQueue = taskQueue;
        this.scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Moderation background service is running.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        logger.LogInformation("Moderation background service is stopping.");
    }
}