using AutoMapper;
using RenStore.Application.Dto.ShoppingCart;

namespace RenStore.Application.Common.Mappings.ShoppingCart;

public class AddToCartMappingProfile : Profile
{
    public AddToCartMappingProfile()
    {
        CreateMap<AddToCartDto, AddToCartCommand>();
    }
}