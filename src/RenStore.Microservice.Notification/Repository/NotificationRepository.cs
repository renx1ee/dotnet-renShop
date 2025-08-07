using Microsoft.EntityFrameworkCore;
using RenStore.Microservice.Notification.Data;

namespace RenStore.Microservice.Notification.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext context;
    
    public NotificationRepository(NotificationDbContext context) => 
        this.context = context;
    
    public async Task AddAsync(Models.Notification notification, CancellationToken cancellationToken)
    {
        await context.Notifications.AddAsync(notification, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Models.Notification notification, CancellationToken cancellationToken)
    {
        context.Update(notification);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var notification = await this.GetByIdAsync(userId, cancellationToken)
            ?? throw new Exception("Notification not found");
        
        context.Remove(notification);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Models.Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Notifications
           .FirstOrDefaultAsync(notification => 
               notification.Id == id,
               cancellationToken)
            ?? null;
    }
    
    public async Task<IList<Models.Notification>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Notifications
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IList<Models.Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Notifications
            .Where(notification => 
                notification.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}