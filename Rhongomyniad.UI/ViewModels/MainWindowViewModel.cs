using CommunityToolkit.Mvvm.ComponentModel;

namespace Rhongomyniad.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    public MainWindowViewModel(GameListViewModel gameListViewModel)
    {
        _currentViewModel = gameListViewModel;
    }
}