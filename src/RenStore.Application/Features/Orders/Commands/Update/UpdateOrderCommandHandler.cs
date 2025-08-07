using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandHandler
    : IRequestHandler<UpdateOrderCommand>
{
    private readonly ILogger logger;
    private readonly IOrderRepository orderRepository;

    public UpdateOrderCommandHandler(
        ILogger<UpdateOrderCommandHandler> logger,
        IOrderRepository orderRepository)
    {
        this.logger = logger;
        this.orderRepository = orderRepository;
    }
    
    public async Task Handle(UpdateOrderCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(UpdateOrderCommandHandler)}");

        var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);

        if (order.ApplicationUserId != request.ApplicationUserId.ToString())
            throw new NotFoundException(typeof(Order), request.Id);
        
        if(request.Address != "string")
            order.Address = request.Address;
        if(request.Country != "string")
            order.Country = request.Country;
        if(request.City != "string")
            order.City = request.City;
        
        await orderRepository.UpdateAsync(order, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(UpdateOrderCommandHandler)}");
    }
}