using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Wrench.Models;
using Wrench.Services;

namespace PackageManager.ViewModels;

public class MainPageViewModel : ViewModelBase
{
    public MainPageViewModel()
    {
        var fwProvider = new FirmwareProvider();
        
        FirmwareSources = new(new FirmwareSourcesProvider().GetSources());

        FirmwareCollection = new(FirmwareSources.SelectMany(x => fwProvider.GetFirmware(x)));
        
    }

    public ObservableCollection<FirmwareSource>? FirmwareSources { get; set; }
    public ObservableCollection<Firmware>? FirmwareCollection { get; set; }
}
