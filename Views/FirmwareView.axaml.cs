using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using PackageManager.Models;
using PackageManager.ViewModels;

namespace PackageManager.Views;

public partial class FirmwareView : UserControl
{
    public FirmwareView()
    {
        InitializeComponent();
    }

    private async void AddPackageButton_Clicked(object s, RoutedEventArgs e)
    {
        var top = TopLevel.GetTopLevel(this);

        var dirs = await top!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Choose package directory",
            AllowMultiple = true,
        });

        if (DataContext is FirmwareViewModel d)
            d.AddPackage(dirs);
    }
}