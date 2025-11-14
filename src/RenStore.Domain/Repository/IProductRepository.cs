using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;

public interface IProductRepository
{
    Task<Guid> CreateAsync(
        ProductEntity product,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductEntity product,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        ProductEntity product,
        CancellationToken cancellationToken);

    Task<IEnumerable<ProductEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false,
        ProductSortBy sortBy = ProductSortBy.Id);

    Task<ProductEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ProductEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}