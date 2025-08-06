using AutoMapper;
using RenStore.Application.Dto.Seller;

namespace RenStore.Application.Common.Mappings.Seller;

public class UpdateSellerMappingProfile : Profile
{
    public UpdateSellerMappingProfile()
    {
        CreateMap<UpdateSellerDto, UpdateSellerCommand>();
    }
}