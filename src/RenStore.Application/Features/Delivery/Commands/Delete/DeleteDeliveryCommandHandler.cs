using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Delivery.Command.Delete;

public class DeleteDeliveryCommandHandler : IRequestHandler<DeleteDeliveryCommand>
{
    private readonly ILogger<DeleteDeliveryCommandHandler> logger;
    private readonly IDeliveryRepository repository;
    
    public DeleteDeliveryCommandHandler(
        ILogger<DeleteDeliveryCommandHandler> logger,
        IDeliveryRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }
    
    public async Task Handle(DeleteDeliveryCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(DeleteDeliveryCommandHandler)}");

        try
        {
            await repository.DeleteAsync(request.Id, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError($"Error with {typeof(DeleteDeliveryCommandHandler)} Error message: {e.Message}");
        }
        
        logger.LogInformation($"Handled {nameof(DeleteDeliveryCommandHandler)}");
    }
}