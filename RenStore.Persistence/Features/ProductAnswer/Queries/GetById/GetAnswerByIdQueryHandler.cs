using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductAnswer.Queries.GetById;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductAnswer.Queries.GetById;

public class GetAnswerByIdQueryHandler 
    : IRequestHandler<GetAnswerByIdQuery, GetAnswerByIdVm>
{
    private readonly ILogger<GetAnswerByIdQueryHandler> logger;
    private readonly IProductAnswerRepository answerRepository;
    
    public GetAnswerByIdQueryHandler(
        ILogger<GetAnswerByIdQueryHandler> logger,
        IProductAnswerRepository answerRepository)
    {
        this.logger = logger;
        this.answerRepository = answerRepository;
    }
    
    public async Task<GetAnswerByIdVm> Handle(GetAnswerByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetAnswerByIdQueryHandler)}");

        try
        {
            var item = await answerRepository.GetByIdAsync(request.Id, cancellationToken);

            var result = new GetAnswerByIdVm(
                request.Id, 
                item!.CreatedDate, 
                item.SellerName, 
                item.SellerId, 
                item.Message,
                item.ProductQuestionId);
            
            logger.LogInformation($"Handled {nameof(GetAnswerByIdQueryHandler)}");

            return result;
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Handled error: {nameof(GetAnswerByIdQueryHandler)} Message: {ex.Message}, Answer Id: {request.Id}");
        }
        
        return null;
    }
}