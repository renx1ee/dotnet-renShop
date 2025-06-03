using AutoMapper;

namespace RenStore.Application.Common.Mappings.Category;

public class GetCategoryByNameMappingProfile : Profile
{
    public GetCategoryByNameMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Category, CategoryByNameVm>();
    }
}