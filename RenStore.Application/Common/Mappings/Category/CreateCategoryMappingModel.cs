using AutoMapper;
using RenStore.Application.Features.Category.Commands.Create;
using RenStore.Domain.Dto.Category;

namespace RenStore.Application.Common.Mappings.Category;

public class CreateCategoryMappingModel : Profile
{
    public CreateCategoryMappingModel()
    {
        CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        CreateMap<CreateCategoryCommand, RenStore.Domain.Entities.Category>();
    }
}