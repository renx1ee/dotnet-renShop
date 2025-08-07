using AutoMapper;
using RenStore.Application.Dto.Review;

namespace RenStore.Application.Common.Mappings.Review;

public class UpdateReviewMappingProfile : Profile
{
    public UpdateReviewMappingProfile()
    {
        CreateMap<UpdateReviewDto, UpdateReviewCommand>();
    }
}