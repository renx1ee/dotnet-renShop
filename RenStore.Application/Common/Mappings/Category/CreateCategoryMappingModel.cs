using AutoMapper;
using RenStore.Application.Dto.Category;
using RenStore.Application.Features.Category.Commands.Create;

namespace RenStore.Application.Common.Mappings.Category;

public class CreateCategoryMappingModel : Profile
{
    public CreateCategoryMappingModel()
    {
        CreateMap<CreateCategoryDto, CreateCategoryCommand>();
        CreateMap<CreateCategoryCommand, RenStore.Domain.Entities.Category>();
    }
}