using AutoMapper;
using RenStore.Application.Dto.Category;

namespace RenStore.Application.Common.Mappings.Category;

public class UpdateCategoryMappingProfile: Profile
{
    public UpdateCategoryMappingProfile()
    {
        CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();
    }
}