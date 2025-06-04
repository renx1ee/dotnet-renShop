using System.Threading.Channels;
using RenStore.Application.Interfaces;

namespace RenStore.Persistence.Queues;

public class BackgroundModerationQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<CancellationToken, ValueTask>> queue;

    public BackgroundModerationQueue(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
    }
    
    public ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}