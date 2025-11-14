using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;

public interface IProductVariantRepository
{
    Task<Guid> CreateAsync(
        ProductVariantEntity productVariant,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductVariantEntity productVariant,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        ProductVariantEntity productVariant,
        CancellationToken cancellationToken);

    Task<IEnumerable<ProductVariantEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false,
        ProductVariantSortBy sortBy = ProductVariantSortBy.Id,
        bool? isAvailable = null);

    Task<ProductVariantEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<ProductVariantEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}