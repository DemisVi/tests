using System;
using PackageManager.Models;

namespace PackageManager.ViewModels;

public class FirmwareViewModel : ViewModelBase
{
    public FirmwareViewModel(Firmware fw) => Firmware = fw;

    public Firmware Firmware { get; set; }
    public string ModelId { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;
}
