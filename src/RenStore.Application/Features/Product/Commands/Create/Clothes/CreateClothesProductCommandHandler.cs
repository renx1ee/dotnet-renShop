using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;
using RenStore.Application.Services;

namespace RenStore.Application.Features.Product.Commands.Create.Clothes;

public class CreateClothesProductCommandHandler 
    : IRequestHandler<CreateClothesProductCommand, Guid>
{
    private readonly IProductRepository productRepository;
    private readonly ProductService productService;
    private readonly ISellerRepository sellerRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    private readonly ILogger logger;
    
    public CreateClothesProductCommandHandler(
        ProductService productService,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISellerRepository sellerRepository,
        IMapper mapper,
        ILogger<CreateClothesProductCommandHandler> logger)
    {
        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
        this.sellerRepository = sellerRepository;
        this.productService = productService;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<Guid> Handle(CreateClothesProductCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateClothesProductCommand)}");

        // TODO:
        var category = await categoryRepository.FindByNameAsync("Clothes", cancellationToken);
        var seller = await sellerRepository.FindByIdAsync(request.SellerId, cancellationToken);

        if (category is null || seller is null)
            return Guid.Empty;
        
        var discount = await productService.CalculateDiscountByPriceAsync(
                request.Price, request.OldPrice);
        
        var article = await productService.CreateArticleAsync();
        
        var product = mapper.Map<Domain.Entities.Product>(request);
        
        product.CategoryId = category.Id;
        product.CategoryName = category.Name; 
        product.SellerName = seller.Name; 
        product.Discount = discount;
        product.ProductDetail.Article = article; 
        product.CreatedDate = DateTime.UtcNow;
        // TODO:
        product.ImagePath = "sources/images/products/clothes/" + product.Id + ".jpg";
        product.ImagePath = "sources/images/products/clothes/mini/" + product.Id + ".jpg";
        
        var result = await productRepository.CreateAsync(product, cancellationToken);
    
        logger.LogInformation($"Handled {nameof(CreateClothesProductCommand)}");

        return result;
    }
}