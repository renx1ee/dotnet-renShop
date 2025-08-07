using AutoMapper;

namespace RenStore.Application.Common.Mappings.Category;

public class GetCategoryByIdMappingProfile : Profile
{
    public GetCategoryByIdMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Category, CategoryByIdVm>();
    }
}