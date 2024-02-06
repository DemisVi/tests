using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PackageManager.ViewModels;
using Avalonia.Platform.Storage;
using Avalonia.Markup.Xaml;
using PackageManager.Models;
using System.IO;

namespace PackageManager.Views;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();
    }

    private async void AddFirmwareButton_Clicked(object s, RoutedEventArgs e)
    {
        var top = TopLevel.GetTopLevel(this);

        var dirs = await top!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Choose firmware directory",
            SuggestedStartLocation = TopLevel.GetTopLevel(this)!.StorageProvider.TryGetFolderFromPathAsync("./").Result,
        });

        var target = ((s as Button)?.DataContext as FirmwareSource)?.SubfolderName;

        if (DataContext is MainPageViewModel d && dirs is not null and { Count: > 0 })
            d.AddFirmware(dirs[0], target!);
    }

    private async void OpenButton_Clicked(object s, RoutedEventArgs e)
    {
        var top = TopLevel.GetTopLevel(this);

        var dir = await top!.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Choose sources directory",
            SuggestedStartLocation = TopLevel.GetTopLevel(this)!.StorageProvider.TryGetFolderFromPathAsync("C:/").Result,
        });

        if (DataContext is MainPageViewModel d && dir is not null and { Count: > 0 })
            d.Open(dir[0]);
    }
}