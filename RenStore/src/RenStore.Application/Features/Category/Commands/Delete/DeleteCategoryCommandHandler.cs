using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Repository;

namespace RenStore.Application.Features.Category.Commands.Delete;

public class DeleteCategoryCommandHandler 
    : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ILogger<DeleteCategoryCommandHandler> logger;
    private readonly ICategoryRepository categoryRepository;

    public DeleteCategoryCommandHandler(
        ILogger<DeleteCategoryCommandHandler> logger,
        ICategoryRepository categoryRepository)
    {
        this.logger = logger;
        this.categoryRepository = categoryRepository;
    }
    
    public async Task Handle(DeleteCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(DeleteCategoryCommandHandler)}");
          
        await categoryRepository.DeleteAsync(request.Id, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(DeleteCategoryCommandHandler)}");
    }
}