using FluentValidation;

namespace RenStore.Application.Features.ProductAnswer.Command.Create;

public class CreateProductAnswerCommandValidator : AbstractValidator<CreateProductAnswerCommand>
{
    public CreateProductAnswerCommandValidator()
    {
        RuleFor(answer => answer.Message)
            .MaximumLength(500)
            .MaximumLength(10)
            .WithMessage("");
        
        RuleFor(answer => answer.ProductQuestionId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .WithMessage("");
    }
}