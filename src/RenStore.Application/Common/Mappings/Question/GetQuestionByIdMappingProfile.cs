using AutoMapper;
using RenStore.Domain.Entities;

namespace RenStore.Application.Common.Mappings.Question;

public class GetQuestionByIdMappingProfile : Profile
{
    public GetQuestionByIdMappingProfile()
    {
        CreateMap<ProductQuestion, GetProductQuestionByIdVm>();
    }
}