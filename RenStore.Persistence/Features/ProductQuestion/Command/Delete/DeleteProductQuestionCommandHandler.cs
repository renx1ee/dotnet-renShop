using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductQuestion.Command.Delete;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductQuestion.Command.Delete;

public class DeleteProductQuestionCommandHandler : IRequestHandler<DeleteProductQuestionCommand>
{
    private readonly ILogger<DeleteProductQuestionCommandHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    
    public DeleteProductQuestionCommandHandler(
        ILogger<DeleteProductQuestionCommandHandler> logger,
        IProductQuestionRepository productQuestionRepository)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
    }

    public async Task Handle(DeleteProductQuestionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(DeleteProductQuestionCommandHandler)}");
        
        try
        {
            await productQuestionRepository.DeleteAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError($"Delete question handling Error. Message: {ex.Message} QuestionId: {request.Id}");
        }
        
        logger.LogInformation($"Handled {nameof(DeleteProductQuestionCommandHandler)}");
    }
}