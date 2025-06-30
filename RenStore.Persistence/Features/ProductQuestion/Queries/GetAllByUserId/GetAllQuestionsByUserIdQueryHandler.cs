using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllByUserId;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductQuestion.Queries.GetAllByUserId;

public class GetAllQuestionsByUserIdQueryHandler 
    : IRequestHandler<GetAllQuestionsByUserIdQuery, IEnumerable<GetAllQuestionsByUserIdVm>>
{
    private readonly ILogger<GetAllQuestionsByUserIdQueryHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;

    public GetAllQuestionsByUserIdQueryHandler(
        ILogger<GetAllQuestionsByUserIdQueryHandler> logger,
        IProductQuestionRepository productQuestionRepository)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
    }

    public async Task<IEnumerable<GetAllQuestionsByUserIdVm>> Handle(
        GetAllQuestionsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllQuestionsByUserIdQueryHandler)}");
        
        try
        {
            var question = await this.productQuestionRepository
                .FindByUserIdAsync(
                    userId: request.UserId,
                    count: request.Count,
                    cancellationToken: cancellationToken);
            
            var result = question.Select(item => 
                new GetAllQuestionsByUserIdVm(
                    item.Id,
                    item.ProductId,
                    item.ApplicationUserId,
                    item.CreatedDate,
                    item.UserName,
                    item.Message
            ));
            
            logger.LogInformation($"Handled {nameof(GetAllQuestionsByUserIdQueryHandler)}");
            
            return result;
        }
        catch (Exception e)
        {
            logger.LogInformation($"Handling Error with {nameof(GetAllQuestionsByUserIdQueryHandler)} Message: {e.Message} User ID: {request.UserId}");
        }
        
        logger.LogInformation($"Handled {nameof(GetAllQuestionsByUserIdQueryHandler)}");

        return null;
    }
}