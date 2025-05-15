using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RenStore.Application.BackgroundServices;

public class ExampleBackgroundService : BackgroundService
{
    private readonly ILogger<ExampleBackgroundService> logger;

    public ExampleBackgroundService(ILogger<ExampleBackgroundService> logger)
    {
        this.logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: доделать
        
        
        
        throw new NotImplementedException();
    }
}