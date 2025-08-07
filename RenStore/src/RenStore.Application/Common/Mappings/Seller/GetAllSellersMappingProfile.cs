using AutoMapper;

namespace RenStore.Application.Common.Mappings.Seller;

public class GetAllSellersMappingProfile : Profile
{
    public GetAllSellersMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Seller, GetSellerByIdVm>();
    }
}