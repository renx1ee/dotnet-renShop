using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.Category.Commands.Delete;
using RenStore.Application.Repository;
using RenStore.Persistence.Features.Product.Command.Delete;

namespace RenStore.Persistence.Features.Category.Command.Delete;

public class DeleteCategoryCommandHandler 
    : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ILogger<DeleteProductCommandHandler> logger;
    private readonly ICategoryRepository categoryRepository;

    public DeleteCategoryCommandHandler(
        ILogger<DeleteProductCommandHandler> logger,
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