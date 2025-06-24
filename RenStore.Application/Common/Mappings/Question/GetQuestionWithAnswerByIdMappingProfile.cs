using AutoMapper;
using RenStore.Application.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;
using RenStore.Domain.Entities;

namespace RenStore.Application.Common.Mappings.Question;

public class GetQuestionWithAnswerByIdMappingProfile : Profile
{
    GetQuestionWithAnswerByIdMappingProfile()
    {
        CreateMap<ProductQuestion, GetQuestionWithAnswerByIdVm>();
    }
}