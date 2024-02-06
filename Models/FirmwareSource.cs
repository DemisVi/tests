using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PackageManager.DataTypes;

namespace PackageManager.Models;

public class FirmwareSource
{
    public string Name { get; init; } = string.Empty;
    public string SubfolderName { get; set; } = string.Empty;
    public DeviceType DeviceType { get; init; } = DeviceType.Unknown;
    public ObservableCollection<Firmware>? Firmware { get; set; }
}
