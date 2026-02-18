using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Persistence;

public class InMemoryGameRepository : IGameRepository
{
    private readonly List<Game> _games = new();

    public Task<IReadOnlyList<Game>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Game>>(_games.ToList().AsReadOnly());
    }

    public Task<Game?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_games.FirstOrDefault(g => g.Id == id));
    }

    public Task AddAsync(Game game)
    {
        _games.Add(game);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Game game)
    {
        var index = _games.FindIndex(g => g.Id == game.Id);
        if (index >= 0)
        {
            _games[index] = game;
        }
        return Task.CompletedTask;
    }

    public Task<bool> RemoveAsync(Guid id)
    {
        var game = _games.FirstOrDefault(g => g.Id == id);
        if (game != null)
        {
            _games.Remove(game);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(_games.Any(g => g.Id == id));
    }
}
