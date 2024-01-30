using System;

namespace PackageManager.ViewModels;

public class FirmwareViewModel : ViewModelBase
{
    public string ModelId { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;
}
