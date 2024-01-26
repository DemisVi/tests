using System;
using System.Collections.Generic;
using PackageManager.DataTypes;

namespace PackageManager.Models;

public class Package
{
    public string ModelName { get; set; } = string.Empty;
    public string VersionName { get; set; } = string.Empty;
    public string PackagePath { get; set; } = string.Empty;
    public DeviceType DeviceType { get; set; }
}
