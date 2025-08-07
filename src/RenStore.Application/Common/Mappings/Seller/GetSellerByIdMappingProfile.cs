using AutoMapper;

namespace RenStore.Application.Common.Mappings.Seller;

public class GetSellerByIdMappingProfile : Profile
{
    public GetSellerByIdMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Seller, GetSellerByIdVm>();
    }
}