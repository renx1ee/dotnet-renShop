using AutoMapper;

namespace RenStore.Application.Common.Mappings.Order;

public class GetOrderByIdMappingProfile : Profile
{
    public GetOrderByIdMappingProfile()
    {
        CreateMap<RenStore.Domain.Entities.Order, GetOrderByIdVm>();
    }
}