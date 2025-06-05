using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using RenStore.Application.Interfaces;

namespace RenStore.Application.BackgroundsTasks;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<CancellationToken, ValueTask>> queue;
    private readonly ILogger<BackgroundTaskQueue> logger;

    public BackgroundTaskQueue(int capacity, 
        ILogger<BackgroundTaskQueue> logger)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
        this.logger = logger;
    }

    public async ValueTask QueueBackgroundWorkItemAsync(
        Func<CancellationToken, ValueTask> workItem) 
    {
        if(workItem is null) 
            throw new ArgumentNullException(nameof(workItem));

        await queue.Writer.WriteAsync(workItem);
        logger.LogInformation("Work item queued.");
    }

    public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
        CancellationToken cancellationToken)
    {
        var workItem = await queue.Reader.ReadAsync(cancellationToken);
        logger.LogInformation("Work item dequeued.");
        return workItem;
    }
}