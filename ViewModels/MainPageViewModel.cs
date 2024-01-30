using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using PackageManager.Models;
using PackageManager.Services;
using ReactiveUI;

namespace PackageManager.ViewModels;

public class MainPageViewModel : ViewModelBase
{
    private FirmwareSourcesProvider fwsProvider = new FirmwareSourcesProvider();
    private object? selectedItem;

    // private FileSystemWatcher watcher;

    public MainPageViewModel()
    {
        FirmwareSources = new(fwsProvider.GetSources());
    }

    public ViewModelBase? FwViewModel { get; set; }
    public ObservableCollection<FirmwareSource>? FirmwareSources { get; set; }
    public object? SelectedItem
    {
        get => selectedItem;
        set
        {
            selectedItem = value;
            if (value is Firmware f) 
            {
                FwViewModel = new FirmwareViewModel() { ModelId = f.ModelName, Serial = f.FirmwarePath };
                this.RaisePropertyChanged(nameof(FwViewModel));
            }
        }
    }
}
