using FluentValidation;

namespace RenStore.Application.Features.Product.Commands.Create.Shoes;

public class CreateShoesProductCommandValidator 
    : AbstractValidator<CreateShoesProductCommand>
{
    public CreateShoesProductCommandValidator()
    {
        RuleFor(shoes => shoes.ProductName)
            .MaximumLength(150)
            .MinimumLength(10)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(shoes => shoes.Description)
            .MaximumLength(500)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(shoes => shoes.InStock)
            .NotNull();

        RuleFor(shoes => shoes.ImagePath)
            .MaximumLength(500)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(shoes => shoes.SellerId)
            .NotEmpty()
            .NotNull();

        RuleFor(shoes => shoes.ProductDetail.Article)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ProductDetail.Brend)
            .MaximumLength(25)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ProductDetail.CountryOfManufacture)
            .MaximumLength(25)
            .WithMessage("");
        
        RuleFor(shoes => shoes.ProductDetail.ModelFeatures)
            .MaximumLength(25)
            .WithMessage("");
        
        RuleFor(shoes => shoes.ProductDetail.DecorativeElements)
            .MaximumLength(50)
            .WithMessage("");
        
        RuleFor(shoes => shoes.ProductDetail.Equipment)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(shoes => shoes.ProductDetail.QuantityPerPackage)
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ProductDetail.Composition)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ShoesProduct.FullnessOfShoes)
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ShoesProduct.ShoeInsoleMaterial)
            .NotNull()
            .WithMessage("");

        RuleFor(shoes => shoes.ShoesProduct.ShoeLiningMaterial)
            .NotNull()
            .WithMessage("");
                
        RuleFor(shoes => shoes.ShoesProduct.ShoeSoleMaterial)
            .NotNull()
            .WithMessage("");
                
        RuleFor(shoes => shoes.ShoesProduct.SoleFasteningMethod)
            .NotNull()
            .WithMessage("");
                
        RuleFor(shoes => shoes.ShoesProduct.TypeOfFastener)
            .NotNull()
            .WithMessage("");
                
        RuleFor(shoes => shoes.ShoesProduct.SoleHeight)
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ShoesProduct.Gender)
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ShoesProduct.Season)
            .NotNull()
            .WithMessage("");
        
        RuleFor(shoes => shoes.ShoesProduct.TakingCareOfThings)
            .NotNull()
            .WithMessage("");
    }
}