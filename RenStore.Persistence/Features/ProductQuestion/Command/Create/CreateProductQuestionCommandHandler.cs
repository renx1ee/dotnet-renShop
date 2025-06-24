using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RenStore.Application.Common.Exceptions;
using RenStore.Application.Features.ProductQuestion.Command.Create;
using RenStore.Application.Repository;
using RenStore.Domain.Entities;

namespace RenStore.Persistence.Features.ProductQuestion.Command.Create;

public class CreateProductQuestionCommandHandler 
    : IRequestHandler<CreateProductQuestionCommand, Guid>
{
    private readonly ILogger<CreateProductQuestionCommandHandler> logger;
    private readonly IProductQuestionRepository productQuestionRepository;
    private readonly IProductRepository productRepository;
    private readonly UserManager<ApplicationUser> userManager;

    public CreateProductQuestionCommandHandler(
        ILogger<CreateProductQuestionCommandHandler> logger,
        IProductQuestionRepository productQuestionRepository,
        IProductRepository productRepository,
        UserManager<ApplicationUser> userManager)
    {
        this.logger = logger;
        this.productQuestionRepository = productQuestionRepository;
        this.productRepository = productRepository;
        this.userManager = userManager;
    }

    public async Task<Guid> Handle(CreateProductQuestionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(CreateProductQuestionCommandHandler)}");

        var product = await productRepository.GetByIdAsync(
            request.ProductId, cancellationToken);

        var user = await userManager.FindByIdAsync(request.ApplicationUserId)
            ?? throw new NotFoundException(typeof(ApplicationUser), request.ApplicationUserId);

        var question = new Domain.Entities.ProductQuestion
        {
            Product = product,
            ProductId = request.ProductId,
            ApplicationUserId = request.ApplicationUserId,
            UserName = user.Name ?? "Anonymous User", // TODO:
            CreatedDate = DateTime.UtcNow, 
            Message = request.Message,
        };

        var result = await this.productQuestionRepository
            .CreateAsync(question, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(CreateProductQuestionCommandHandler)}");
        
        return result;
    }
}