using System.Data;

namespace RenStore.Microservice.Payment.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext dbContext;
    private readonly string connectionString; 

    public PaymentRepository(
        PaymentDbContext dbContext,
        IConfiguration configuration)
    {
        this.dbContext = dbContext;
        connectionString = configuration.GetConnectionString("PaymentConnection")!;
    }
    
    public async Task<Guid> CreateAsync(Models.Payment payment, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(payment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return payment.Id;
    }
    
    public async Task UpdateAsync(Models.Payment payment, CancellationToken cancellationToken)
    {
        dbContext.Update(payment);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var payment = await this.GetByIdAsync(id, cancellationToken);

        if (payment is not null)
        {
            dbContext.Remove(id);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        
        throw new Exception();
    }
    
    public async Task<IEnumerable<Models.Payment>> GetAllAsync(Models.Payment payment, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }

    public async Task<Models.Payment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }

    public async Task<IEnumerable<Models.Payment>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }
    
    public async Task<Models.Payment> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }
    
    public async Task<IEnumerable<Models.Payment>> GetBySellerIdAsync(uint sellerId, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }
}