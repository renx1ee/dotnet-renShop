using RenStore.Microservice.Notification.Models;

namespace RenStore.Microservice.Notification.Repository;

public class MessageRepository : IMessageRepository
{
    public async Task AddAsync(Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Message>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Message>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Message>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Message>> GetByChatIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Message>> GetBySenderIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}