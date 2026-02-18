using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Domain.Interfaces;

public interface IGameRepository
{
    Task<IReadOnlyList<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(Guid id);
    Task AddAsync(Game game);
    Task UpdateAsync(Game game);
    Task<bool> RemoveAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
