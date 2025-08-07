using Microsoft.EntityFrameworkCore;
using RenStore.Microservice.Notification.Data;
using RenStore.Microservice.Notification.Models;

namespace RenStore.Microservice.Notification.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly NotificationDbContext context;
    
    public MessageRepository(NotificationDbContext context) => 
        this.context = context;
    public async Task AddAsync(Message message, CancellationToken cancellationToken)
    {
        await context.Messages.AddAsync(message, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Message message, CancellationToken cancellationToken)
    {
        context.Update(message);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var notification = await this.GetByIdAsync(id, cancellationToken)
            ?? throw new Exception("Notification not found");
        
        context.Remove(notification);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<Message>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Messages
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Message>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Messages
            .Where(message => message.Id == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Message>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Messages
            .Where(message => message.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Message>> GetByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await context.Messages
            .Where(message => message.ChatId == chatId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Message>> GetBySenderIdAsync(Guid senderId, CancellationToken cancellationToken)
    {
        return await context.Messages
            .Where(message => message.SenderId == senderId)
            .ToListAsync(cancellationToken);
    }
}