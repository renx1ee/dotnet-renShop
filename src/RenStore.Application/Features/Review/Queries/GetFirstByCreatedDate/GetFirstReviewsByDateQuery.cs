using MediatR;

namespace RenStore.Application.Features.Review.Queries.GetFirstByCreatedDate;

public class GetFirstReviewsByDateQuery : IRequest<IList<GetFirstReviewsByDateVm>>
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }
}