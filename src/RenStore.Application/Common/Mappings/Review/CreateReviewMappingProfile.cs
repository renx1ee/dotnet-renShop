using AutoMapper;
using RenStore.Application.Dto.Review;

namespace RenStore.Application.Common.Mappings.Review;

public class CreateReviewMappingProfile : Profile
{
    public CreateReviewMappingProfile()
    {
        CreateMap<CreateReviewDto, CreateReviewCommand>();
        
        CreateMap<CreateReviewCommand, RenStore.Domain.Entities.Review>();
    }
}