using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.ProductAnswer.Command.Delete;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.ProductAnswer.Command.Delete;

public class DeleteProductAnswerCommandHandler : IRequestHandler<DeleteProductAnswerCommand>
{
    private readonly ILogger<DeleteProductAnswerCommandHandler> logger;
    private readonly IProductAnswerRepository productAnswerRepository;

    public DeleteProductAnswerCommandHandler(
        ILogger<DeleteProductAnswerCommandHandler> logger,
        IProductAnswerRepository productAnswerRepository)
    {
        this.logger = logger;
        this.productAnswerRepository = productAnswerRepository;
    }

    public async Task Handle(DeleteProductAnswerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(DeleteProductAnswerCommandHandler)}");

        try
        {
            await this.productAnswerRepository.DeleteAsync(request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Handling Error {nameof(DeleteProductAnswerCommandHandler)} Message: {ex.Message}, AnswerId: {request.Id}");
        }
        
        logger.LogInformation($"Handled {nameof(DeleteProductAnswerCommandHandler)}");
    }
}