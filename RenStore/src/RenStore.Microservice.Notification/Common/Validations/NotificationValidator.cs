using FluentValidation;

namespace RenStore.Microservice.Notification.Common.Validations;

public class NotificationValidator : AbstractValidator<Models.Notification>
{
    public NotificationValidator()
    {
        RuleFor(notification => notification.UserId)
            .NotEqual(Guid.Empty)
            .WithMessage("");

        RuleFor(notification => notification.Type)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(notification => notification.Subject)
            .NotEmpty()
            .NotNull()
            .MinimumLength(15)
            .WithMessage("");
        
        RuleFor(notification => notification.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(4)
            .WithMessage("");
        
        RuleFor(notification => notification.Status)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(notification => notification.CreatedAt)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(notification => notification.ReadAt)
            .Null()
            .WithMessage("");
    }
}