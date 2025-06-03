using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RenStore.Application.Services;

namespace RenStore.Application.BackgroundServices;

public class PriceCounterBackgroundService : BackgroundService
{
    private readonly ILogger<PriceCounterBackgroundService> logger;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public PriceCounterBackgroundService(
        ILogger<PriceCounterBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.logger = logger;
        this.serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var productService = scope.ServiceProvider.GetService<ProductService>();
            }
            catch
            {
                // Ignored
            }
        }
    }

    private async Task CalculatePrice(CancellationToken cancellationToken)
    {
        
    }
}