using AutoMapper;

namespace RenStore.Application.Common.Mappings.Product;

public class GetProductByArticleMappingProfile : Profile
{
    public GetProductByArticleMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Product, GetProductByArticleVm>();
    }
}