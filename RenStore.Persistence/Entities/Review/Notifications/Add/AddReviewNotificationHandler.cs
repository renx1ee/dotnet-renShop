using MediatR;
using RenStore.Application.Entities.Review.Notifications.Add;

namespace RenStore.Persistence.Entities.Review.Notifications.Add;

public class AddReviewNotificationHandler : INotificationHandler<AddReviewNotification>
{
    
    public async Task Handle(AddReviewNotification notification, CancellationToken cancellationToken)
    {
        
    }
}