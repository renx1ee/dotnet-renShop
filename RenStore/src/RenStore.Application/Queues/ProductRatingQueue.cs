using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace RenStore.Application.Queues;

public class ProductRatingQueue : IProductRatingQueue, IDisposable
{
    private readonly ILogger<ProductRatingQueue> logger;
    private readonly Channel<Guid> channel;

    public ProductRatingQueue(
        ILogger<ProductRatingQueue> logger)
    {
        this.logger = logger;

        var options = new UnboundedChannelOptions()
        {
            SingleReader = false,
            SingleWriter = false
        };
        channel = Channel.CreateUnbounded<Guid>(options);
    }
    
    public async Task EnqueueAsync(Guid productId, CancellationToken cancellationToken)
    {
        logger.LogInformation("Product rating queue method is starting.");
        
        try
        {
            await channel.Writer.WriteAsync(productId, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError("");
            throw;
        }
        
        logger.LogInformation("Product rating queue method is stopping.");
    }

    public async Task<Guid> DequeueAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("");
            
            return await channel.Reader.ReadAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError("");
            throw;
        }
    }

    public void Dispose()
    {
        channel.Writer.Complete();
    }
}