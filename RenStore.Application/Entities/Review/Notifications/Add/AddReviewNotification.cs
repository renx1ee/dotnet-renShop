using RenStore.Application.Data.Common.Mediatr;

namespace RenStore.Application.Entities.Review.Notifications.Add;

public class AddReviewNotification : NotificationBase
{
    protected AddReviewNotification(string subject, string content, bool isCoupleted, string addressFrom, string addressTo) : base(subject, content, isCoupleted, addressFrom, addressTo)
    {
    }
}