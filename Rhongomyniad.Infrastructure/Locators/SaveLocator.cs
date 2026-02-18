using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Locators;

public class SaveLocator : ISaveLocator
{
    public Task<Result<SaveProfile>> LocateAsync(Game game)
    {
        // TODO: Implement actual save file location logic
        return Task.FromResult(Result<SaveProfile>.Failure("Not implemented"));
    }

    public bool Supports(Game game)
    {
        return true; // Support all games for now
    }
}
