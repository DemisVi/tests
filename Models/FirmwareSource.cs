using System;
using PackageManager.DataTypes;

namespace PackageManager.Models;

public class FirmwareSource
{
    public string Name { get; init; } = string.Empty;
    public string SubfolderName { get; init; } = string.Empty;
    public DeviceType DeviceType { get; init; } = DeviceType.Unknown;
}
