using FluentValidation;

namespace RenStore.Application.Features.Review.Commands.Update;

public class UpdateReviewCommandValidation : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidation()
    {
        RuleFor(review => review.Message)
            .MinimumLength(5)
            .MaximumLength(150)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(review => review.Rating)
            .NotNull()
            .WithMessage("");
        
        RuleFor(review => review.ApplicationUserId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("");
    }
}