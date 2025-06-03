using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RenStore.Application.Entities.Category.Commands.Update;
using RenStore.Application.Repository;

namespace RenStore.Persistence.Entities.Category.Command.Update;

public class UpdateCategoryCommandHandler 
    : IRequestHandler<UpdateCategoryCommand>
{
    public ICategoryRepository categoryRepository { get; set; }
    public readonly ILogger<UpdateCategoryCommandHandler> logger;
    private readonly IMapper mapper;

    public UpdateCategoryCommandHandler(IMapper mapper,
        ILogger<UpdateCategoryCommandHandler> logger,
        ICategoryRepository categoryRepository)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling {nameof(UpdateCategoryCommandHandler)}");

        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (category.Name != "string")
            category.Name = request.Name;
        if (category.Description != "string")
            category.Description = request.Description;

        await categoryRepository.UpdateAsync(category, cancellationToken);
        
        logger.LogInformation($"Handled {nameof(UpdateCategoryCommandHandler)}");
    }
}