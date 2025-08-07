using FluentValidation;

namespace RenStore.Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(order => order.Address)
            .MinimumLength(5)
            .MaximumLength(150)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(order => order.Country)
            .MinimumLength(5)
            .MaximumLength(45)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(order => order.City)
            .MinimumLength(5)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(order => order.Amount)
            .NotNull()
            .WithMessage("");
        
        RuleFor(order => order.ApplicationUserId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("");
        
        RuleFor(order => order.ProductId)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("");
    }
}