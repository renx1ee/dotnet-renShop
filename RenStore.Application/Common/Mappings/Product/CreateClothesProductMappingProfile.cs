using AutoMapper;
using RenStore.Application.Dto.Product;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Application.Common.Mappings.Product;

public class CreateClothesProductMappingProfile : Profile
{
    public CreateClothesProductMappingProfile()
    {
        #region CreateProductDto To Command
        CreateMap<CreateClothesProductDto, CreateClothesProductCommand>()
            .ForMember(model => model.ProductName, opt =>
                opt.MapFrom(item => item.ProductName))
            .ForMember(model => model.Price, opt => 
                opt.MapFrom(item => item.Price))
            .ForMember(model => model.OldPrice,opt => 
                opt.MapFrom(item => item.OldPrice))
            .ForMember(model => model.Description, opt => 
                opt.MapFrom(item => item.Description))
            .ForMember(model => model.InStock, opt => 
                opt.MapFrom(item => item.InStock))
            .ForMember(model => model.ImagePath, opt => 
                opt.MapFrom(item => item.ImagePath))
            .ForMember(model => model.ImageMiniPath, opt => 
                opt.MapFrom(item => item.ImageMiniPath))
            .ForMember(model => model.ImageMiniPath, opt => 
                opt.MapFrom(item => item.ImagesListPath))
            .ForMember(model => model.SellerId, opt => 
                opt.MapFrom(item => item.SellerId))
            .ForMember(model => model.ProductDetail, opt =>
                opt.MapFrom(item => new ProductDetail
                {
                    Brend = item.Brend,
                    CountryOfManufacture = item.CountryOfManufacture,
                    ModelFeatures = item.ModelFeatures,
                    DecorativeElements = item.DecorativeElements,
                    Equipment = item.Equipment,
                    QuantityPerPackage = item.QuantityPerPackage,
                    Composition = item.Composition,
                    Color = item.Color,
                    TypeOfPackaging = item.TypeOfPackaging
                }))
            .ForMember(model => model.ClothesProduct, opt =>
                opt.MapFrom(item => new ClothesProduct
                {
                    Neckline = item.Neckline,
                    TheCut = item.TheCut,
                    TypeOfPockets = item.TypeOfPockets,
                    Gender = item.Gender,
                    Season = item.Season,
                    TakingCareOfThings = item.TakingCareOfThings,
                    Sizes = item.Sizes
                }));
        #endregion
        
        CreateMap<CreateClothesProductCommand, RenStore.Domain.Entities.Product>();
    }
}