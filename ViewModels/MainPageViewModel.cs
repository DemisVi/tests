using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia.Platform.Storage;
using DynamicData;
using Microsoft.VisualBasic;
using PackageManager.Models;
using PackageManager.Services;
using ReactiveUI;

namespace PackageManager.ViewModels;

public class MainPageViewModel : ViewModelBase
{
    private FirmwareSourcesProvider fwsProvider = new FirmwareSourcesProvider();
    private object? selectedItem;
    private ObservableCollection<FirmwareSource>? firmwareSources;

    // private FileSystemWatcher watcher;

    public MainPageViewModel()
    {
        var directories = Directory.GetDirectories("C:\\");
        var file = string.Empty;

        foreach (var i in directories)
        {
            try
            {
                file = Directory.GetFiles(i, "Sources.json")[0];
                break;
            }
            catch (Exception) { }
        }

        try
        {
            FirmwareSources = new(fwsProvider.GetSources(file));
        }
        catch (Exception)
        { }
    }

    public ViewModelBase? FwViewModel { get; set; }
    public ObservableCollection<FirmwareSource>? FirmwareSources
    {
        get => firmwareSources;
        set => this.RaiseAndSetIfChanged(ref firmwareSources, value);
    }
    public object? SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;

            switch (value)
            {
                case Firmware f:
                    FwViewModel = new FirmwareViewModel(f);
                    break;
                case Package p:
                    FwViewModel = new PackageViewModel(p);
                    break;
                default:
                    break;
            }

            this.RaisePropertyChanged(nameof(FwViewModel));
        }
    }

    public void Open(IStorageFolder dir)
    {
        FirmwareSources = new(fwsProvider.GetSources(new DirectoryInfo(dir.Path.LocalPath)));
    }

    public void AddFirmware(IStorageFolder dir, string target)
    {

        var targetPath = Path.Combine(target, dir.Name);
        var sourcePath = dir.Path.LocalPath;

        var dirs = Directory.EnumerateDirectories(sourcePath, "*", SearchOption.AllDirectories);
        var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories);

        foreach (var i in dirs)
            Directory.CreateDirectory(i.Replace(sourcePath, targetPath));

        foreach (var f in files)
            File.Copy(f, f.Replace(sourcePath, targetPath), true);

        var fw = FirmwareSources?.SingleOrDefault(x => x.SubfolderName == target);
        if (fw is not null)
        {
            fw.Firmware?.Clear();
            fw.Firmware?.AddRange(new FirmwareProvider().GetFirmware(fw));
        }
    }
}
