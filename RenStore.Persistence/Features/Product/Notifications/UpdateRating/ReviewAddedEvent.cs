using MediatR;

namespace RenStore.Persistence.Features.Product.Notifications.UpdateRating;

public record ReviewAddedEvent(Guid ProductId) : INotification;