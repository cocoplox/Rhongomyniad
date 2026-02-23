using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rhongomyniad.Application.Services;
using Rhongomyniad.Domain.Context;
using Rhongomyniad.Domain.Interfaces;
using Rhongomyniad.Infrastructure.Scanners;
using Rhongomyniad.Infrastructure.Locators;
using Rhongomyniad.Infrastructure.Persistence;
using Rhongomyniad.Infrastructure.Services;

namespace Rhongomyniad.UI.Services;

public static class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services)
    {
        //HttpClients
        services.AddHttpClient<ISteamStoreService, SteamStoreService>(client =>
        {
            client.BaseAddress = new Uri("https://store.steampowered.com/api/");
        });
        //Register OS based services
        if (OperatingSystem.IsWindows())
        {
            services.AddScoped<ISteamLibraryPathProvider, WindowsSteamLibraryPathProvider>();
        }

        services.AddScoped<ISteamFilesParser, SteamFilesParser>();
        // Register concrete scanners
        services.AddScoped<SteamGameScanner>();
        services.AddScoped<EpicGameScanner>();
        
        // Register CompositeGameScanner as factory to avoid circular dependency
        //Esto en realidad sobra
        
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

    public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var appName = configuration["Application:Name"] ?? throw new InvalidDataException("Falta Application:Name en appsettings");
        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(basePath,  appName);
        Directory.CreateDirectory(dbPath);
        
        var db = Path.Combine(dbPath, $"{appName}.db");

        services.AddDbContext<RhongomyniadDbContext>(opts =>
            opts.UseSqlite("Data Source=" + db));
    }
}
