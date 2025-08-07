using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Category.Queries.GetCategoryByName;

public class GetCategoryByNameQueryHandler
    : IRequestHandler<GetCategoryByNameQuery, CategoryByNameVm>
{
    private readonly ILogger<GetCategoryByIdQueryHandler> logger;
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetCategoryByNameQueryHandler(IMapper mapper,
        ILogger<GetCategoryByIdQueryHandler> logger,
        ICategoryRepository categoryRepository)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.categoryRepository = categoryRepository;
    }
    
    public async Task<CategoryByNameVm> Handle(GetCategoryByNameQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetCategoryByIdQueryHandler)}");
        
        var category = await categoryRepository.GetByNameAsync(request.Name, cancellationToken);
        
        var result = mapper.Map<CategoryByNameVm>(category); 
        
        logger.LogInformation($"Handled {nameof(GetCategoryByIdQueryHandler)}"); 

        return result;
    }
}