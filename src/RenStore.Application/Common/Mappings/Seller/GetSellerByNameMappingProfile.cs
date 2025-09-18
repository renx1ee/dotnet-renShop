using AutoMapper;

namespace RenStore.Application.Common.Mappings.Seller;

public class GetSellerByNameMappingProfile : Profile
{
    public GetSellerByNameMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Seller, GetSellerByNameVm>();
    }
}