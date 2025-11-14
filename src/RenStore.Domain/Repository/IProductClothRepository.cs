using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;

public interface IProductClothRepository
{
    Task<Guid> CreateAsync(
        ProductClothEntity cloth,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductClothEntity cloth,
        CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductClothEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        ProductClothSortBy sortBy = ProductClothSortBy.Id,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false);

    Task<ProductClothEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ProductClothEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}