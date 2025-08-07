using AutoMapper;
using RenStore.Domain.Entities;

namespace RenStore.Application.Common.Mappings.Answer;

public class GetAnswerMappingProfile : Profile
{
    public GetAnswerMappingProfile()
    {
        CreateMap<ProductAnswer, GetQuestionWithAnswerByIdVm>();
    }
}