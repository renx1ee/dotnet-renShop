using MediatR;

namespace RenStore.Application.Features.Category.Commands.Update;

public class UpdateCategoryCommand : IRequest
{
    public int Id{ get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}