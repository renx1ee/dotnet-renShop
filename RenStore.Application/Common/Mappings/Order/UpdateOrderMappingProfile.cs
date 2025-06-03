using AutoMapper;
using RenStore.Domain.Dto.Order;

namespace RenStore.Application.Common.Mappings.Order;

public class UpdateOrderMappingProfile : Profile
{
    public UpdateOrderMappingProfile()
    {
        CreateMap<UpdateOrderDto, UpdateOrderCommand>();
    }
}