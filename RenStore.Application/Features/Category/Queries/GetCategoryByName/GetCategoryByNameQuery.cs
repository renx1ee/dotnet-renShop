using MediatR;

namespace RenStore.Application.Features.Category.Queries.GetCategoryByName;

public class GetCategoryByNameQuery : IRequest<CategoryByNameVm>
{
    public string Name { get; set; }
}