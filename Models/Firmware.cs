using System;
using System.Collections.Generic;
using Avalonia.Data.Converters;

namespace PackageManager.Models;

public class Firmware
{
    public string ModelName { get; set; } = string.Empty;
    public string FirmwarePath { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public bool EnableFactoryCFG { get; set; }
    public IEnumerable<Package>? Packages { get; set; }
}
