using FluentValidation;

namespace RenStore.Application.Features.Product.Commands.Create.Clothes;

public class CreateClothesProductCommandValidator 
    : AbstractValidator<CreateClothesProductCommand>
{
    public CreateClothesProductCommandValidator()
    {
        RuleFor(clothes => clothes.ProductName)
            .MaximumLength(150)
            .MinimumLength(10)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(clothes => clothes.Description)
            .MaximumLength(500)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(clothes => clothes.InStock)
            .NotNull();

        RuleFor(clothes => clothes.ImagePath)
            .MaximumLength(500)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(clothes => clothes.SellerId)
            .NotEmpty()
            .NotNull();

        RuleFor(clothes => clothes.ProductDetail.Article)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ProductDetail.Brend)
            .MaximumLength(25)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ProductDetail.CountryOfManufacture)
            .MaximumLength(25)
            .WithMessage("");
        
        RuleFor(clothes => clothes.ProductDetail.ModelFeatures)
            .MaximumLength(25)
            .WithMessage("");
        
        RuleFor(clothes => clothes.ProductDetail.DecorativeElements)
            .MaximumLength(50)
            .WithMessage("");
        
        RuleFor(clothes => clothes.ProductDetail.Equipment)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull()
            .WithMessage("");

        RuleFor(clothes => clothes.ProductDetail.QuantityPerPackage)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ProductDetail.Composition)
            .MaximumLength(50)
            .NotEmpty()
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.Neckline)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.TheCut)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.TypeOfPockets)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.Gender)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.Season)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.TakingCareOfThings)
            .NotNull()
            .WithMessage("");
        
        RuleFor(clothes => clothes.ClothesProduct.Sizes)
            .NotNull()
            .WithMessage("");
    }
}