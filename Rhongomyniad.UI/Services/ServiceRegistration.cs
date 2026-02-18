using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Rhongomyniad.Application.Services;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Infrastructure.Scanners;
using Rhongomyniad.Infrastructure.Locators;
using Rhongomyniad.Infrastructure.Persistence;

namespace Rhongomyniad.UI.Services;

public static class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Register concrete scanners
        services.AddScoped<SteamGameScanner>();
        services.AddScoped<EpicGameScanner>();
        
        // Register CompositeGameScanner as factory to avoid circular dependency
        services.AddScoped<CompositeGameScanner>(provider =>
        {
            var scanners = new List<IGameScanner>
            {
                provider.GetRequiredService<SteamGameScanner>(),
                provider.GetRequiredService<EpicGameScanner>()
            };
            return new CompositeGameScanner(scanners);
        });
        
        // Register IGameScanner to use CompositeGameScanner
        services.AddScoped<IGameScanner>(provider => 
            provider.GetRequiredService<CompositeGameScanner>());
        
        // Register locators
        services.AddScoped<ISaveLocator, SaveLocator>();
        services.AddScoped<IConfigLocator, ConfigLocator>();
        
        // Register repository (using in-memory for now)
        services.AddSingleton<IGameRepository, InMemoryGameRepository>();
        
        // Application services
        services.AddScoped<GameDiscoveryService>();
        
        // ViewModels
        services.AddSingleton<ViewModels.MainWindowViewModel>();
        services.AddSingleton<ViewModels.GameListViewModel>();
    }
}
