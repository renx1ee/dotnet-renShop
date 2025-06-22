using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.ProductQuestion.Queries.GetAll;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.ProductQuestion.Queries.GetAll;

public class GetAllQuestionsCommandHandler 
    : IRequestHandler<GetAllQuestionsCommand, IEnumerable<GetAllQuestionsVm>>
{
    private readonly ILogger<GetAllQuestionsCommandHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;

    public GetAllQuestionsCommandHandler(
        ILogger<GetAllQuestionsCommandHandler> logger,
        IProductQuestionRepository productQuestionRepository)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
    }
    
    public async Task<IEnumerable<GetAllQuestionsVm>> Handle(GetAllQuestionsCommand request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllQuestionsCommandHandler)}.");

        try
        {
            var items = await this.productQuestionRepository
                .GetAllAsync(cancellationToken);

            var result = items.Select(item =>
                new GetAllQuestionsVm(
                    item.Id,
                    item.ProductId,
                    item.ApplicationUserId,
                    item.CreatedDate,
                    item.UserName,
                    item.Message)
                ).ToList();
            
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError($"Handling error. message: {ex.Message}");
        }
        
        logger.LogInformation($"Handled {nameof(GetAllQuestionsCommandHandler)}.");

        return null;
    }
}