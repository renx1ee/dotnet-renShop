using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Orders.Queries.GetById;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Orders.Queries.GetById;

public class GetOrderByIdQueryHandler
 : IRequestHandler<GetOrderByIdQuery, GetOrderByIdVm>
{
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IOrderRepository orderRepository;

    public GetOrderByIdQueryHandler(
        ILogger<GetOrderByIdQueryHandler> logger,
        IMapper mapper,
        IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<GetOrderByIdVm> Handle(GetOrderByIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetOrderByIdQueryHandler)}");
        
        var order = await orderRepository.GetByIdAsync(request.Id, cancellationToken);
        
        var result = mapper.Map<GetOrderByIdVm>(order);
        
        logger.LogInformation($"Handling {nameof(GetOrderByIdQueryHandler)}");

        return result;
    } 
}