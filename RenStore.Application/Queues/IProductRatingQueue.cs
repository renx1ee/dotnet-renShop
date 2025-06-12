namespace RenStore.Application.Queues;

public interface IProductRatingQueue
{
    Task EnqueueAsync(Guid productId, CancellationToken cancellationToken);
    Task<Guid> DequeueAsync(CancellationToken cancellationToken);
}