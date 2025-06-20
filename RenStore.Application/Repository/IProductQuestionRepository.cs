using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;

public interface IProductQuestionRepository
{
    Task<Guid> CreateAsync(ProductQuestion question, CancellationToken cancellationToken);
    Task UpdateAsync(ProductQuestion question, CancellationToken cancellationToken);
    Task DeleteAsync(Guid questionId, CancellationToken cancellationToken);
    Task<IEnumerable<ProductQuestion>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductQuestion?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductQuestion?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductQuestion>> FindByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    Task<IEnumerable<ProductQuestion>> GetByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    Task<IEnumerable<ProductQuestion>> FindByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
    Task<IEnumerable<ProductQuestion>> GetByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
}