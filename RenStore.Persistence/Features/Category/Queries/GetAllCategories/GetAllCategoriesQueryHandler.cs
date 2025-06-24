using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Category.Queries.GetAllCategories;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Category.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler
    : IRequestHandler<GetCategoriesListQuery, List<CategoryLookupDto>>
{
    private readonly ILogger<GetAllCategoriesQueryHandler> logger;
    private readonly ICategoryRepository categoryRepository;

    public GetAllCategoriesQueryHandler(
        ILogger<GetAllCategoriesQueryHandler> logger,
        ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
        this.logger = logger;
    }
    
    public async Task<List<CategoryLookupDto>> Handle(GetCategoriesListQuery request,
        CancellationToken cancellationToken) 
    {
        logger.LogInformation($"Handling {nameof(GetAllCategoriesQueryHandler)}");
        
        var items = await categoryRepository.GetAllAsync(cancellationToken);
        
        var result = items.Select(category => 
            new CategoryLookupDto(
                category.Id,
                category.Name,
                category.Description,
                category.ImagePath)
            )
            .ToList();
        
        logger.LogInformation($"Handled {nameof(GetAllCategoriesQueryHandler)}");
        
        return result;
    }
}