using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Queries.GetAllDeliveries;

public class GetAllDeliveriesQueryHandler 
    : IRequestHandler<GetAllDeliveriesQuery, IList<GetAllDeliveriesVm>>
{
    private readonly ILogger<GetAllDeliveriesQueryHandler> logger;
    private readonly IDeliveryRepository deliveryRepository;
    
    public GetAllDeliveriesQueryHandler(
        IDeliveryRepository deliveryRepository,
        ILogger<GetAllDeliveriesQueryHandler> logger)
    {
        this.logger = logger;
        this.deliveryRepository = deliveryRepository;
    }

    public async Task<IList<GetAllDeliveriesVm>> Handle(GetAllDeliveriesQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllDeliveriesQueryHandler)}");

        try
        {
            var deliveries = await deliveryRepository.GetAllAsync(cancellationToken);

            return deliveries.Select(delivery =>
                new GetAllDeliveriesVm(
                    delivery.Id,
                    delivery.UserId,
                    delivery.CreatedDate,
                    delivery.Status,
                    delivery.Address))
                .ToList();
        }
        catch (Exception e)
        {
            logger.LogError($"Error with {nameof(GetAllDeliveriesQueryHandler)}");    
        }
        
        logger.LogInformation($"Handled {nameof(GetAllDeliveriesQueryHandler)}");
        
        return null;
    }
}