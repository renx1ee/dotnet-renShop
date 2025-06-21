using FluentValidation;

namespace RenStore.Application.Entities.ProductQuestion.Command.Create;

public class CreateProductQuestionCommandValidator : AbstractValidator<CreateProductQuestionCommand>
{
    public CreateProductQuestionCommandValidator()
    {
        RuleFor(question => question.Message)
            .MinimumLength(10)
            .MaximumLength(500)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(question => question.ApplicationUserId)
            .NotEqual(Guid.Empty.ToString())
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(question => question.ProductId)
            .NotEqual(Guid.Empty)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
    }
}