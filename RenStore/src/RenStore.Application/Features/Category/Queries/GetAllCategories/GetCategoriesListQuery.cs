using MediatR;

namespace RenStore.Application.Features.Category.Queries.GetAllCategories;

public class GetCategoriesListQuery : IRequest<List<CategoryLookupDto>>
{
}