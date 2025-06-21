using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;

public interface IProductAnswerRepository
{
    Task<Guid> CreateAsync(ProductAnswer question, CancellationToken cancellationToken);
    Task UpdateAsync(ProductAnswer question, CancellationToken cancellationToken);
    Task DeleteAsync(Guid questionId, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAnswer>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductAnswer?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductAnswer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAnswer>> FindByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAnswer>> GetByProductIdAsync(Guid productId, uint count, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAnswer>> FindByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAnswer>> GetByUserIdAsync(string userId, uint count, CancellationToken cancellationToken);
    Task<ProductAnswer> FindByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
    Task<ProductAnswer> GetByQuestionIdAsync(Guid questionId, CancellationToken cancellationToken);
}