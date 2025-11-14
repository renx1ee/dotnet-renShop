using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;

public interface IProductDetailRepository
{
    Task<Guid> CreateAsync(
        ProductDetailEntity detail,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductDetailEntity detail,
        CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductDetailEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        ProductDetailSortBy sortBy = ProductDetailSortBy.Id,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false);

    Task<ProductDetailEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ProductDetailEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}