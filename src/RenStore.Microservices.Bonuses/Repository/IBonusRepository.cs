using RenStore.Microservices.Bonuses.Models;

namespace RenStore.Microservices.Bonuses.Repository;

public interface IBonusRepository
{
    Task CreateAsync(Bonus bonus, CancellationToken cancellationToken);
    Task UpdateAsync(Bonus bonus, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Bonus?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Bonus>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Bonus>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Bonus>> GetBySellerIdAsync(Guid sellerId, CancellationToken cancellationToken);
    Task<IEnumerable<Bonus>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
}