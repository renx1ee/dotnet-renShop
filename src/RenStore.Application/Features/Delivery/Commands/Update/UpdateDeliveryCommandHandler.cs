using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Delivery.Command.Update;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Commands.Update;

public class UpdateDeliveryCommandHandler : IRequestHandler<UpdateDeliveryCommand>
{
    private readonly ILogger<UpdateDeliveryCommandHandler> logger;
    private readonly IDeliveryRepository deliveryRepository;
    
    public UpdateDeliveryCommandHandler(
        ILogger<UpdateDeliveryCommandHandler> logger,
        IDeliveryRepository deliveryRepository)
    {
        this.logger = logger;
        this.deliveryRepository = deliveryRepository;
    }

    public async Task Handle(UpdateDeliveryCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(UpdateDeliveryCommandHandler)}");

        try
        {

        }
        catch (Exception e)
        {
            logger.LogError($"Handled {nameof(UpdateDeliveryCommandHandler)}");
        }
        
        logger.LogInformation($"Handled {nameof(UpdateDeliveryCommandHandler)}");
    }
}