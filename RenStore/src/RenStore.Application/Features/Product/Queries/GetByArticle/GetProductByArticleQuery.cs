using MediatR;

namespace RenStore.Application.Features.Product.Queries.GetByArticle;

public class GetProductByArticleQuery : IRequest<GetProductByArticleVm>
{
    public int Article { get; set; }
}