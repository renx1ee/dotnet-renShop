using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductQuestion.Queries.GetById;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductQuestion.Queries.GetById;

public class GetProductQuestionByIdQueryHandler 
    : IRequestHandler<GetProductQuestionByIdQuery, GetProductQuestionByIdVm>
{
    private readonly ILogger<GetProductQuestionByIdQueryHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    private readonly IMapper mapper;

    public GetProductQuestionByIdQueryHandler(
        ILogger<GetProductQuestionByIdQueryHandler> logger,
        IProductQuestionRepository productQuestionRepository,
        IMapper mapper)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
        this.mapper = mapper;
    }
    
    public async Task<GetProductQuestionByIdVm> Handle(GetProductQuestionByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetProductQuestionByIdQueryHandler)}");

        try
        {
            var item = 
                await this.productQuestionRepository
                    .GetByIdAsync(id: request.Id, 
                    cancellationToken: cancellationToken);

            var result = mapper.Map<GetProductQuestionByIdVm>(item);
            
            logger.LogInformation($"Handled {nameof(GetProductQuestionByIdQueryHandler)}");

            return result;
        }
        catch (Exception e)
        {
            logger.LogInformation($"Handling Error with {nameof(GetProductQuestionByIdQueryHandler)} Message: {e.Message}, Id: {request.Id}");
        }

        return null;
    }
}