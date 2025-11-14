using RenStore.Domain.Entities;
using RenStore.Domain.Enums.Sorting;

namespace RenStore.Domain.Repository;


public interface IProductAttributeRepository
{
    Task<Guid> CreateAsync(
        ProductAttributeEntity attribute,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        ProductAttributeEntity attribute,
        CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ProductAttributeEntity>> FindAllAsync(
        CancellationToken cancellationToken,
        ProductAttributeSortBy sortBy = ProductAttributeSortBy.Id,
        uint pageCount = 25,
        uint page = 1,
        bool descending = false);

    Task<ProductAttributeEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ProductAttributeEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}