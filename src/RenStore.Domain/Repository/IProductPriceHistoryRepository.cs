using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;

public interface IProductPriceHistoryRepository
{
    Task<Guid> CreateAsync(
        ProductPriceHistoryEntity priceHistory,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductPriceHistoryEntity priceHistory,
        CancellationToken cancellationToken);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductPriceHistoryEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        ProductPriceHistorySortBy sortBy = ProductPriceHistorySortBy.Id,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false);

    Task<ProductPriceHistoryEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ProductPriceHistoryEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}