using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rhongomyniad.Application.DTOs;
using Rhongomyniad.Application.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Rhongomyniad.UI.ViewModels;

public partial class GameListViewModel : ViewModelBase
{
    private readonly GameDiscoveryService _gameDiscoveryService;

    [ObservableProperty]
    private ObservableCollection<GameSummaryDto> _games = new();

    [ObservableProperty]
    private bool _isScanning;

    [ObservableProperty]
    private string _statusMessage = "Ready";

    public GameListViewModel(GameDiscoveryService gameDiscoveryService)
    {
        _gameDiscoveryService = gameDiscoveryService;
    }

    [RelayCommand]
    private async Task ScanGamesAsync()
    {
        IsScanning = true;
        StatusMessage = "Scanning for games...";

        try
        {
            var games = await _gameDiscoveryService.DiscoverGamesAsync();
            Games.Clear();
            foreach (var game in games)
            {
                Games.Add(game);
            }
            StatusMessage = $"Found {games.Count} games";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
        finally
        {
            IsScanning = false;
        }
    }

    [RelayCommand]
    private async Task LoadGamesAsync()
    {
        try
        {
            var games = await _gameDiscoveryService.GetAllGamesAsync();
            Games.Clear();
            foreach (var game in games)
            {
                Games.Add(game);
            }
            StatusMessage = $"Loaded {games.Count} games";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }
}
