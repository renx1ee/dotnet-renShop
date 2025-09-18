using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Commands.Create;

public class CreateDeliveryCommandHandler 
    : IRequestHandler<CreateDeliveryCommand, Guid>
{
    private readonly ILogger<CreateDeliveryCommandHandler> logger;
    private readonly IDeliveryRepository repository;
    
    public CreateDeliveryCommandHandler(
        ILogger<CreateDeliveryCommandHandler> logger,
        IDeliveryRepository repository)
    {
        this.repository = repository;
        this.logger = logger;
    }
    
    public async Task<Guid> Handle(CreateDeliveryCommand request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateDeliveryCommandHandler)}");

        try
        {
            // TODO: сделать проверку валидности доставки

            var delivery = new Domain.Entities.Delivery()
            {
                
            };

            var result = await repository.CreateAsync(delivery, cancellationToken);
            
            return result;
        }
        catch (Exception e)
        {
            logger.LogError($"Error with {typeof(CreateDeliveryCommandHandler)} Error message: {e.Message}");
        }
        
        logger.LogInformation($"Handled delivery for {nameof(CreateDeliveryCommandHandler)}");
        
        return Guid.Empty;
    }   
}