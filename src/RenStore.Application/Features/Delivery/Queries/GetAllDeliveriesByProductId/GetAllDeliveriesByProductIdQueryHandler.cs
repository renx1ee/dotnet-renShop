using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Delivery.Queries.GetAllDeliveries;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesByProductId;

public class GetAllDeliveriesByProductIdQueryHandler 
    : IRequestHandler<GetAllDeliveriesByProductIdQuery, IList<GetAllDeliveriesByProductIdVm>>
{
    private readonly ILogger<GetAllDeliveriesByProductIdQueryHandler> logger;
    private readonly IDeliveryRepository deliveryRepository;
    
    public GetAllDeliveriesByProductIdQueryHandler(
        ILogger<GetAllDeliveriesByProductIdQueryHandler> logger,
        IDeliveryRepository deliveryRepository)
    {
        this.logger = logger;
        this.deliveryRepository = deliveryRepository;
    }
    
    public async Task<IList<GetAllDeliveriesByProductIdVm>> Handle(GetAllDeliveriesByProductIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllDeliveriesByProductIdQueryHandler)}");
        
        try
        {
            var deliveries = await deliveryRepository.FindByProductIdAsync(request.ProductId, cancellationToken);
            
            return deliveries.Select(delivery =>
                new GetAllDeliveriesByProductIdVm(
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
            logger.LogError($"Handling error with {nameof(GetAllDeliveriesByProductIdQueryHandler)}. Error message: {e.Message}.");
        }
            
        logger.LogInformation($"Handled {nameof(GetAllDeliveriesByProductIdQueryHandler)}");
        
        return null;
    }
}