using MediatR;

namespace RenStore.Persistence.Entities.Product.Notifications.UpdateRating;

public record ReviewAddedEvent(Guid ProductId) : INotification;