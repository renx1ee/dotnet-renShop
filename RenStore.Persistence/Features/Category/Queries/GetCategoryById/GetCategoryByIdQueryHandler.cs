using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Category.Queries.GetCategoryById;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.Category.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler
    : IRequestHandler<GetCategoryByIdQuery, CategoryByIdVm>
{
    private readonly ILogger<GetCategoryByIdQueryHandler> logger;
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetCategoryByIdQueryHandler(IMapper mapper,
        ILogger<GetCategoryByIdQueryHandler> logger,
        ICategoryRepository categoryRepository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.categoryRepository = categoryRepository;
    }
    
    public async Task<CategoryByIdVm> Handle(GetCategoryByIdQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetCategoryByIdQueryHandler)}");
        
        var category = await categoryRepository.FindByIdAsync(request.Id, cancellationToken);
        
        var result = mapper.Map<CategoryByIdVm>(category); 
        
        logger.LogInformation($"Handled {nameof(GetCategoryByIdQueryHandler)}");

        return result;
    }
}