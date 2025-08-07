using MediatR;

namespace RenStore.Application.Features.Category.Queries.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<CategoryByIdVm>
{
    public int Id { get; set; }
}