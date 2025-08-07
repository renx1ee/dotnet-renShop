using MediatR;

namespace RenStore.Application.Features.Product.Queries.GetSortedByRating;

public class GetSortedProductsByRatingQuery : IRequest<IList<GetSortedProductsByRatingVm>>
{
    public bool Descending { get; set; }
}