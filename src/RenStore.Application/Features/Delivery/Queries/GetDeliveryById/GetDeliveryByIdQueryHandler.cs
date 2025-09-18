using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;
using RenStore.Domain.Enums;

namespace RenStore.Application.Features.Delivery.Queries.GetDeliveryById;

public class GetDeliveryByIdQueryHandler : IRequestHandler<GetDeliveryByIdQuery, GetDeliveryByIdVm>
{
    private readonly ILogger<GetDeliveryByIdQueryHandler> logger;
    private readonly IDeliveryRepository deliveryRepository;
    
    public GetDeliveryByIdQueryHandler(
        ILogger<GetDeliveryByIdQueryHandler> logger,
        IDeliveryRepository deliveryRepository)
    {
        this.logger  = logger;
        this.deliveryRepository = deliveryRepository;
    }
    
    public async Task<GetDeliveryByIdVm> Handle(GetDeliveryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetDeliveryByIdQueryHandler)}");

        try
        {
            var delivery = await deliveryRepository.FindByIdAsync(request.Id, cancellationToken);

            if (delivery is not null)
            {
                var result = new GetDeliveryByIdVm
                (
                    id:  delivery.Id,
                    userId: delivery.UserId,
                    status: delivery.Status,
                    address: delivery.Address,
                    createdDate: delivery.CreatedDate
                );
            
                return result; 
            }
            
            logger.LogError($"Delivery with id {request.Id} was not found");
            
            return null;
        }
        catch (Exception e)
        {
            logger.LogError($"Handling error with {nameof(GetDeliveryByIdQueryHandler)}. Error message: {e.Message}.");
        }
        
        logger.LogInformation($"Handled {nameof(GetDeliveryByIdQueryHandler)}");
        
        return null;
    }
}