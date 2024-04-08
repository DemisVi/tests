using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using PackageManager.Models;
using PackageManager.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

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
            SuggestedStartLocation = TopLevel.GetTopLevel(this)!.StorageProvider.TryGetFolderFromPathAsync("./").Result,
        });

        if (DataContext is FirmwareViewModel d)
            d.AddPackage(dirs);
    }

    private async void RemovePackage_Clicked(object s, RoutedEventArgs e)
    {
        var pack = (s as Button)?.DataContext as Package;
        var msgBox = MessageBoxManager.GetMessageBoxStandard("Warning", $"Confirm {pack?.VersionName} deletion", ButtonEnum.YesNo);
        var res = await msgBox.ShowWindowDialogAsync(this.FindAncestorOfType<Window>());

        if (pack is not null && res == ButtonResult.Yes)
        {
            (DataContext as FirmwareViewModel)?.PerformRemovePackage(pack);
        }
    }
    private async void ArchivePackage_Clicked(object s, RoutedEventArgs e)
    {
        var pack = (s as Button)?.DataContext as Package;
        var msgBox = MessageBoxManager.GetMessageBoxStandard("Warning", $"Confirm moving {pack?.VersionName} to archive", ButtonEnum.YesNo);
        var res = await msgBox.ShowWindowDialogAsync(this.FindAncestorOfType<Window>());

        if (pack is not null && res == ButtonResult.Yes)
        {
            (DataContext as FirmwareViewModel)?.PerformArchivePackage(pack);
        }
    }
}