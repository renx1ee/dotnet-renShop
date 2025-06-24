using MediatR;

namespace RenStore.Persistence.Features.Product.Notifications.UpdateRating;

public class UpdateProductRatingNotificationHandler : INotificationHandler<ReviewAddedEvent>
{
    public async Task Handle(ReviewAddedEvent notification, CancellationToken cancellationToken)
    {
        
    }
}