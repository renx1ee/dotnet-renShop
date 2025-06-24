using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Orders.Commands.Create;
using RenStore.Application.Repository;
using RenStore.Application.Services;
using RenStore.Domain.Entities;
using RenStore.Domain.Enums;

namespace RenStore.Persistence.Features.Orders.Commands.Create;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly ILogger logger;
    private readonly IMapper mapper;
    private readonly IOrderRepository orderRepository;
    private readonly OrderService orderService; 
    private readonly IProductRepository productRepository;

    public CreateOrderCommandHandler(
        ILogger<CreateOrderCommandHandler> logger,
        IMapper mapper,
        IOrderRepository orderRepository,
        OrderService orderService,
        IProductRepository productRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.orderRepository = orderRepository;
        this.orderService = orderService;
        this.productRepository = productRepository;
    }
    
    public async Task<Guid> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateOrderCommandHandler)}");
        
        var order = mapper.Map<Order>(request);
        
        var product = await productRepository.GetByIdAsync(order.ProductId, cancellationToken);
        
        order.Status = DeliveryStatus.AwaitingConfirmation;
        order.ZipCode = await orderService.CreateZipCodeAsync();
        order.OrderTotal = await orderService.GetOrderTotalPrice(product.Price, request.Amount);
        order.CreatedDate = DateTime.UtcNow;
        
        var result = await orderRepository.CreateAsync(order, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(CreateOrderCommandHandler)}");
        
        return result;
    }
}