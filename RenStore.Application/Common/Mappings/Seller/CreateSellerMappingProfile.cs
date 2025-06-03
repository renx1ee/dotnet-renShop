using AutoMapper;
using RenStore.Domain.Dto.Seller;

namespace RenStore.Application.Common.Mappings.Seller;

public class CreateSellerMappingProfile : Profile
{
    public CreateSellerMappingProfile()
    {
        CreateMap<CreateSellerDto, CreateSellerCommand>();
        CreateMap<CreateSellerCommand, RenStore.Domain.Entities.Seller>();
    }
}