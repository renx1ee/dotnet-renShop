using MediatR;
using RenStore.Application.Features.Review.Notifications.Add;

namespace RenStore.Persistence.Features.Review.Notifications.Add;

public class AddReviewNotificationHandler : INotificationHandler<AddReviewNotification>
{
    
    public async Task Handle(AddReviewNotification notification, CancellationToken cancellationToken)
    {
        
    }
}