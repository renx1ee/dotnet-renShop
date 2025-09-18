using AutoMapper;
using RenStore.Application.Dto.Delivery;
using RenStore.Application.Features.Delivery.Commands.Create;

namespace RenStore.Application.Common.Mappings.Delivery;

public class CreateDeliveryMappingProfile : Profile
{
    public CreateDeliveryMappingProfile()
    {
        CreateMap<CreateDeliveryDto, CreateDeliveryCommand>();
    }
}