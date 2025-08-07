using FluentValidation;

namespace RenStore.Application.Features.Review.Commands.Create;

public class CreateReviewCommandValidation : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidation()
    {
        /*RuleFor(review => review.Message)
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
            .NotEqual(String.Empty)
            .NotEqual(Guid.Empty.ToString())
            .WithMessage("");
        
        RuleFor(review => review.ProductId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("");*/
    }
}