using System;
using System.Collections.Generic;
using PackageManager.DataTypes;

namespace PackageManager.Models;

public class FirmwareSource
{
    public string Name { get; init; } = string.Empty;
    public string SubfolderName { get; init; } = string.Empty;
    public DeviceType DeviceType { get; init; } = DeviceType.Unknown;
    public IEnumerable<Firmware>? Firmware { get; set; }
}
