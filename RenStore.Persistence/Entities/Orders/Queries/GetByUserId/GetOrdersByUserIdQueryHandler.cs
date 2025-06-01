using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.Orders.Queries.GetByUserId;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.Orders.Queries.GetByUserId;

public class GetOrdersByUserIdQueryHandler
    : IRequestHandler<GetOrdersByUserIdQuery, IList<GetOrdersByUserIdVm>>
{
    private readonly ILogger logger;
    private readonly IOrderRepository orderRepository;

    public GetOrdersByUserIdQueryHandler(
        ILogger<GetOrdersByUserIdQueryHandler> logger,
        IOrderRepository orderRepository)
    {
        this.logger = logger;
        this.orderRepository = orderRepository;
    }
    
    public async Task<IList<GetOrdersByUserIdVm>> Handle(GetOrdersByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetOrdersByUserIdQueryHandler)}");
        
        var items = await orderRepository.FindByUserIdAsync(request.UserId, cancellationToken); 

        var result = items
            .Select(order => 
                new GetOrdersByUserIdVm(
                    order.Id,
                    order.Address,
                    order.City,
                    order.Country,
                    order.Amount,
                    order.ZipCode,
                    order.Status,
                    order.OrderTotal,
                    order.CreatedDate,
                    order.ApplicationUserId,
                    order.ProductId
                ))
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetOrdersByUserIdQueryHandler)}");

        return result;
    }
}