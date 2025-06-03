using AutoMapper;
using RenStore.Domain.Dto.Review;

namespace RenStore.Application.Common.Mappings.Review;

public class UpdateReviewMappingProfile : Profile
{
    public UpdateReviewMappingProfile()
    {
        CreateMap<UpdateReviewDto, UpdateReviewCommand>();
    }
}