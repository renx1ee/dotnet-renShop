using RenStore.Domain.Entities;

namespace RenStore.Application.Repository;

public interface IColorRepository
{
    Task<int> CreateAsync(Color color, CancellationToken cancellationToken);
    Task UpdateAsync(Color color, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Color>> FindAllAsync(CancellationToken cancellationToken);
    Task<Color?> FindByIdAsync(int id, CancellationToken cancellationToken);
    Task<Color> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Color?> FindByNameAsync(string name, CancellationToken cancellationToken);
    Task<Color> GetByNameAsync(string name, CancellationToken cancellationToken);
}