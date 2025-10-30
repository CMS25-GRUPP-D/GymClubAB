using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.ViewModels;

public class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;
}