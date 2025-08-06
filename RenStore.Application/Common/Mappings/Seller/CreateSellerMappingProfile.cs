using AutoMapper;
using RenStore.Application.Dto.Seller;

namespace RenStore.Application.Common.Mappings.Seller;

public class CreateSellerMappingProfile : Profile
{
    public CreateSellerMappingProfile()
    {
        CreateMap<CreateSellerDto, CreateSellerCommand>();
        CreateMap<CreateSellerCommand, RenStore.Domain.Entities.Seller>();
    }
}