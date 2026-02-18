using Rhongomyniad.Domain.Entities;

namespace Rhongomyniad.Domain.Interfaces;

public interface ISaveLocator
{
    Task<Result<SaveProfile>> LocateAsync(Game game);
    bool Supports(Game game);
}
