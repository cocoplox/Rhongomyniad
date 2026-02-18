using Rhongomyniad.Domain;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Interfaces;

namespace Rhongomyniad.Infrastructure.Locators;

public class ConfigLocator : IConfigLocator
{
    public Task<Result<ConfigProfile>> LocateAsync(Game game)
    {
        // TODO: Implement actual config file location logic
        return Task.FromResult(Result<ConfigProfile>.Failure("Not implemented"));
    }

    public bool Supports(Game game)
    {
        return true; // Support all games for now
    }
}
