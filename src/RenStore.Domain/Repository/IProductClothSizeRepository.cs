using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;

public interface IProductClothSizeRepository
{
    Task<Guid> CreateAsync(
        ProductClothSizeEntity clothSize,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductClothSizeEntity clothSize,
        CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductClothSizeEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        ProductClothSizeSortBy sortBy = ProductClothSizeSortBy.Id,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false);

    Task<ProductClothSizeEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ProductClothSizeEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}