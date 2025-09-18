using MediatR;

namespace RenStore.Application.Features.Category.Commands.Delete;

public class DeleteCategoryCommand : IRequest
{
    public int Id { get; set; }
}