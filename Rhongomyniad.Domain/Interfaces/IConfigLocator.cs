using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Domain.Interfaces;

public interface IConfigLocator
{
    Task<Result<ConfigProfile>> LocateAsync(Game game);
    bool Supports(Game game);
}
