using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductQuestion.Queries.GetAllQuestionsWithAnswers;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductQuestion.Queries.GetAllWithAnswers;

public class GetAllQuestionsWithAnswersQueryHandler 
    : IRequestHandler<GetAllQuestionsWithAnswersQuery, IEnumerable<GetAllQuestionsWithAnswersVm>>
{
    private readonly ILogger<GetAllQuestionsWithAnswersQueryHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    private readonly IProductAnswerRepository productAnswerRepository;
    
    public GetAllQuestionsWithAnswersQueryHandler(
        ILogger<GetAllQuestionsWithAnswersQueryHandler> logger,
        IProductQuestionRepository productQuestionRepository,
        IProductAnswerRepository productAnswerRepository)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
        this.productAnswerRepository = productAnswerRepository;
    }
    
    public Task<IEnumerable<GetAllQuestionsWithAnswersVm>> Handle(GetAllQuestionsWithAnswersQuery request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAllQuestionsWithAnswersQueryHandler)}");
        
        try
        {
            // TODO: 
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Handling Error with: {nameof(GetAllQuestionsWithAnswersQueryHandler)} Message: {ex.Message}");
        }
        
        return null;
    }
}