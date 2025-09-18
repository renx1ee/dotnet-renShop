using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveriesBySellerId;

public class GetAllDeliveriesBySellerIdQueryHandler : IRequestHandler<GetAllDeliveriesBySellerIdQuery, IList<GetAllDeliveriesBySellerIdVm>>
{
    private readonly ILogger<GetAllDeliveriesBySellerIdQueryHandler> logger;
    private readonly IDeliveryRepository deliveryRepository;
    
    public GetAllDeliveriesBySellerIdQueryHandler(
        ILogger<GetAllDeliveriesBySellerIdQueryHandler> logger,
        IDeliveryRepository deliveryRepository)
    {
        this.logger = logger;
        this.deliveryRepository = deliveryRepository;
    }
    
    public async Task<IList<GetAllDeliveriesBySellerIdVm>> Handle(GetAllDeliveriesBySellerIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllDeliveriesBySellerIdQueryHandler)}");
        
        try
        {
            var deliveries = await deliveryRepository
                .FindBySellerIdAsync(request.SellerId, cancellationToken);
            
            return deliveries.Select(delivery => 
                new GetAllDeliveriesBySellerIdVm(
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
            logger.LogError($"Handling error with {nameof(GetAllDeliveriesBySellerIdQueryHandler)}. Error message: {e.Message}");
        }
        
        logger.LogInformation($"Handled {nameof(GetAllDeliveriesBySellerIdQueryHandler)}");
        
        return null;
    }
}