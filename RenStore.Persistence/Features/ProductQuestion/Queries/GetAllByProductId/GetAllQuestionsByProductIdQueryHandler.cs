using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllByProductId;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductQuestion.Queries.GetAllByProductId;

public class GetAllQuestionsByProductIdQueryHandler 
    : IRequestHandler<GetAllQuestionsByProductIdQuery, IEnumerable<GetAllQuestionsByProductIdVm>>
{
    private readonly ILogger<GetAllQuestionsByProductIdQueryHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    
    public GetAllQuestionsByProductIdQueryHandler(
        ILogger<GetAllQuestionsByProductIdQueryHandler> logger,
        IProductQuestionRepository productQuestionRepository)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
    }
    
    public async Task<IEnumerable<GetAllQuestionsByProductIdVm>> Handle(GetAllQuestionsByProductIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllQuestionsByProductIdQueryHandler)}");
        
        try
        {
            var questions =
                await this.productQuestionRepository.GetByProductIdAsync(
                    productId: request.ProductId,
                    count: request.Count,
                    cancellationToken: cancellationToken);

            if (questions.Any())
            {
                var result = questions.Select(item =>
                    new GetAllQuestionsByProductIdVm(
                        item.Id,
                        item.ProductId,
                        item.ApplicationUserId,
                        item.CreatedDate,
                        item.UserName,
                        item.Message
                    ));
                
                logger.LogInformation($"Handled {nameof(GetAllQuestionsByProductIdQueryHandler)}");
                
                return result;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Handling Error with {nameof(GetAllQuestionsByProductIdQueryHandler)} Message: {ex.Message}, ProductId: {request.ProductId}");
        }
        
        logger.LogInformation($"Handled {nameof(GetAllQuestionsByProductIdQueryHandler)}");
        
        return null;
    }
}