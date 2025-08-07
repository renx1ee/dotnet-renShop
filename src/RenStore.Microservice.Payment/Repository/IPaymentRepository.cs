namespace RenStore.Microservice.Payment.Repository;

public interface IPaymentRepository
{
    Task<Guid> CreateAsync(Models.Payment payment, CancellationToken cancellationToken);
    Task UpdateAsync(Models.Payment payment, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Models.Payment>> GetAllAsync(Models.Payment payment, CancellationToken cancellationToken);
    Task<Models.Payment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Models.Payment>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<Models.Payment> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task<IEnumerable<Models.Payment>> GetBySellerIdAsync(uint sellerId, CancellationToken cancellationToken);
}