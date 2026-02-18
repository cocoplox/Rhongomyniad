using Rhongomyniad.Application.DTOs;
using Rhongomyniad.Application.Mapping;
using Rhongomyniad.Domain.Entities;
using Rhongomyniad.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Rhongomyniad.Application.Services;

public sealed class GameDiscoveryService
{
    private readonly IGameScanner _gameScanner;
    private readonly ISaveLocator _saveLocator;
    private readonly IConfigLocator _configLocator;
    private readonly IGameRepository _gameRepository;
    private readonly ILogger<GameDiscoveryService> _logger;

    public GameDiscoveryService(
        IGameScanner gameScanner,
        ISaveLocator saveLocator,
        IConfigLocator configLocator,
        IGameRepository gameRepository,
        ILogger<GameDiscoveryService> logger)
    {
        _gameScanner = gameScanner ?? throw new ArgumentNullException(nameof(gameScanner));
        _saveLocator = saveLocator ?? throw new ArgumentNullException(nameof(saveLocator));
        _configLocator = configLocator ?? throw new ArgumentNullException(nameof(configLocator));
        _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IReadOnlyList<GameSummaryDto>> DiscoverGamesAsync()
    {
        _logger.LogInformation("Starting game discovery");

        var games = await _gameScanner.ScanAsync();
        var discoveredGames = new List<Game>();

        foreach (var game in games)
        {
            try
            {
                var gameWithProfiles = await EnrichGameWithProfilesAsync(game);
                await _gameRepository.AddAsync(gameWithProfiles);
                discoveredGames.Add(gameWithProfiles);
                _logger.LogDebug("Discovered game: {GameName}", game.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process game: {GameName}", game.Name);
            }
        }

        _logger.LogInformation("Discovered {Count} games", discoveredGames.Count);
        return DtoMapper.ToSummaryDtos(discoveredGames);
    }

    private async Task<Game> EnrichGameWithProfilesAsync(Game game)
    {
        SaveProfile? saveProfile = null;
        ConfigProfile? configProfile = null;

        if (_saveLocator.Supports(game))
        {
            var saveResult = await _saveLocator.LocateAsync(game);
            if (saveResult.IsSuccess)
            {
                saveProfile = saveResult.Value;
            }
        }

        if (_configLocator.Supports(game))
        {
            var configResult = await _configLocator.LocateAsync(game);
            if (configResult.IsSuccess)
            {
                configProfile = configResult.Value;
            }
        }

        return game with
        {
            SaveProfile = saveProfile,
            ConfigProfile = configProfile
        };
    }

    public async Task<IReadOnlyList<GameSummaryDto>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllAsync();
        return DtoMapper.ToSummaryDtos(games);
    }

    public async Task<Game?> GetGameByIdAsync(Guid id)
    {
        return await _gameRepository.GetByIdAsync(id);
    }
}
