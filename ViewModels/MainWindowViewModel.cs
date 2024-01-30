namespace PackageManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase model { get; } = new MainPageViewModel();
}
