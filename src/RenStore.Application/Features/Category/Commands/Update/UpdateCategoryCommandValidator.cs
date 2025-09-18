using FluentValidation;

namespace RenStore.Application.Features.Category.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(category => category.Name)
            .MaximumLength(3)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(category => category.Description)
            .MaximumLength(10)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
    }
}