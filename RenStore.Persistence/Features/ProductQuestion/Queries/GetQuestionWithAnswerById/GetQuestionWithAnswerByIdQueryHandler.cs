using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Features.ProductQuestion.Queries.GetQuestionWithAnswerById;

public class GetQuestionWithAnswerByIdQueryHandler 
    : IRequestHandler<GetQuestionWithAnswerByIdQuery, GetQuestionWithAnswerByIdVm>
{
    private readonly ILogger<GetQuestionWithAnswerByIdQueryHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    private readonly IProductAnswerRepository productAnswerRepository;
    private readonly IMapper mapper;
    
    public GetQuestionWithAnswerByIdQueryHandler(
        ILogger<GetQuestionWithAnswerByIdQueryHandler> logger,
        IProductQuestionRepository productQuestionRepository,
        IProductAnswerRepository productAnswerRepository,
        IMapper mapper)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
        this.productAnswerRepository = productAnswerRepository;
        this.mapper = mapper;
    }

    public async Task<GetQuestionWithAnswerByIdVm> Handle(
        GetQuestionWithAnswerByIdQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(GetQuestionWithAnswerByIdQueryHandler)}");

        try
        {
            var question = 
                await this.productQuestionRepository.GetByIdAsync(
                    id: request.Id, 
                    cancellationToken: cancellationToken);
            
            var answer = 
                await this.productAnswerRepository.FindByQuestionIdAsync(
                    questionId: request.Id, 
                    cancellationToken: cancellationToken);

            var result = mapper.Map<GetQuestionWithAnswerByIdVm>(question); 

            if (answer is not null)
                result.Answer = mapper.Map<GetQuestionWithAnswerByIdAnswerVm>(question);
            
            logger.LogInformation($"Handled {nameof(GetQuestionWithAnswerByIdQueryHandler)}");

            return result;
        }
        catch (Exception ex)
        {
            logger.LogInformation($"Handling Error with {nameof(GetQuestionWithAnswerByIdQueryHandler)}. Error: {ex.Message} Question ID: {request.Id}");
        }

        return null;
    }
}