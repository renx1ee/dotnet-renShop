using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductAnswer.Queries.GetAll;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductAnswer.Queries.GetAll;

public class GetAllAnswersQueryHandler 
    : IRequestHandler<GetAllAnswersQuery, IEnumerable<GetAllAnswersVm>>
{
    private readonly ILogger<GetAllAnswersQueryHandler> logger;
    private readonly IProductAnswerRepository answerRepository;

    public GetAllAnswersQueryHandler(
        ILogger<GetAllAnswersQueryHandler> logger,
        IProductAnswerRepository answerRepository)
    {
        this.logger = logger;
        this.answerRepository = answerRepository;
    }
    
    public async Task<IEnumerable<GetAllAnswersVm>> Handle(GetAllAnswersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation($"Handling {nameof(GetAllAnswersQueryHandler)}");

            var answers = await answerRepository.GetAllAsync(cancellationToken);

            var result = answers.Select(answer => 
                new GetAllAnswersVm(
                    answer.Id,
                    answer.CreatedDate,
                    answer.SellerName,
                    answer.SellerId,
                    answer.Message,
                    answer.ProductQuestionId
                    ));

            logger.LogInformation($"Handled {nameof(GetAllAnswersQueryHandler)}");
            
            return result;
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Handling error with {nameof(GetAllAnswersQueryHandler)} Message: {ex.Message}");
        }
        
        return null;
    }
}