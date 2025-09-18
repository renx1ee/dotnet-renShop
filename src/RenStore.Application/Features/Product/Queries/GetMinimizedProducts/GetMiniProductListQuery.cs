using MediatR;

namespace RenStore.Application.Features.Product.Queries.GetMinimizedProducts;

public class GetMiniProductListQuery : IRequest<List<ProductMiniLookupDto>> 
{
}