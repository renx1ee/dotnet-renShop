using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Delivery.Queries.GetDeliveryById;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByUserId;

public class GetAllDeliveriesByUserIdQueryHandler
    : IRequestHandler<GetAllDeliveriesByUserIdQuery, IList<GetAllDeliveriesByUserIdVm>>
{
    private readonly ILogger<GetDeliveryByIdQueryHandler> logger;
    private readonly IDeliveryRepository deliveryRepository;
    
    public GetAllDeliveriesByUserIdQueryHandler(
        ILogger<GetDeliveryByIdQueryHandler> logger,
        IDeliveryRepository deliveryRepository)
    {
        this.logger  = logger;
        this.deliveryRepository = deliveryRepository;
    }
    
    public async Task<IList<GetAllDeliveriesByUserIdVm>> Handle(GetAllDeliveriesByUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllDeliveriesByUserIdQueryHandler)}");
        
        try
        {
            var deliveries = await deliveryRepository
                .FindByUserIdAsync(request.UserId, cancellationToken);
            
            return deliveries.Select(delivery => 
                    new GetAllDeliveriesByUserIdVm(
                        delivery.Id,
                        delivery.UserId,
                        delivery.CreatedDate,
                        delivery.Status,
                        delivery.Address
                    ))
                .ToList();
        }
        catch (Exception e)
        {
            logger.LogError($"Handling error with {nameof(GetAllDeliveriesByUserIdQueryHandler)}. Error message: {e.Message}");
        }
        
        logger.LogInformation($"Handled {nameof(GetAllDeliveriesByUserIdQueryHandler)}");
        
        return null;
    }
}