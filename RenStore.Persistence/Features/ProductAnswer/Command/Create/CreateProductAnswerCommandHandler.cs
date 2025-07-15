using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductAnswer.Command.Create;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductAnswer.Command.Create;

public class CreateProductAnswerCommandHandler : IRequestHandler<CreateProductAnswerCommand, Guid>
{
    private readonly ILogger<CreateProductAnswerCommandHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    private readonly IProductAnswerRepository productAnswerRepository;

    public CreateProductAnswerCommandHandler(
        ILogger<CreateProductAnswerCommandHandler> logger,
        IProductQuestionRepository productQuestionRepository,
        IProductAnswerRepository productAnswerRepository)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
        this.productAnswerRepository = productAnswerRepository;
    }

    public async Task<Guid> Handle(CreateProductAnswerCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateProductAnswerCommandHandler)}");

        // TODO: сделать проверку продавца
        try
        {
            var question = 
                await this.productQuestionRepository
                    .GetByIdAsync(request.ProductQuestionId, cancellationToken);

            var isExist = await this.productAnswerRepository
                .FindByQuestionIdAsync(request.ProductQuestionId, cancellationToken);

            if (isExist is null)
            {
                var answer = new Domain.Entities.ProductAnswer
                {
                    CreatedDate = DateTime.UtcNow,
                    SellerName = "Seller", // TODO:
                    Message = request.Message,
                    ProductQuestionId = request.ProductQuestionId,
                    ProductQuestion = question
                };

                await productAnswerRepository.CreateAsync(answer, cancellationToken);
                
                return answer.Id;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Handling Error {nameof(CreateProductAnswerCommandHandler)} Message: {ex.Message}");
        }
        
        logger.LogInformation($"Handled {nameof(CreateProductAnswerCommandHandler)}");
        
        return Guid.Empty;
    }
}