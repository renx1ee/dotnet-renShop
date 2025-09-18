using AutoMapper;
using RenStore.Application.Dto.Product;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Application.Common.Mappings.Product;

public class CreateShoesProducMappingProfile : Profile
{
    public CreateShoesProducMappingProfile()
    {
        #region CreateProductDto To Command
        CreateMap<CreateShoesProductDto, CreateShoesProductCommand>()
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
            .ForMember(model => model.ShoesProduct, opt =>
                opt.MapFrom(item => new ShoesProduct
                {
                    FullnessOfShoes = item.FullnessOfShoes,
                    ShoeInsoleMaterial = item.ShoeInsoleMaterial,
                    ShoeLiningMaterial = item.ShoeLiningMaterial,
                    ShoeSoleMaterial = item.ShoeSoleMaterial,
                    SoleFasteningMethod = item.SoleFasteningMethod,
                    TypeOfFastener = item.TypeOfFastener,
                    SoleHeight = item.SoleHeight,
                    Gender = item.Gender,
                    Season = item.Season,
                    TakingCareOfThings = item.TakingCareOfThings,
                }));
        #endregion
        
        CreateMap<CreateShoesProductCommand, RenStore.Domain.Entities.Product>();
    }
}