using System;
using System.Collections.Generic;
using Avalonia.Data.Converters;
using PackageManager.Extensions;
using PackageManager.DataTypes;
using System.Collections.ObjectModel;

namespace PackageManager.Models;

public class Firmware
{
    private FactoryCFG? factory;

    public string ModelName { get; set; } = string.Empty;
    public string FirmwarePath { get; set; } = string.Empty;
    public string ModelId
    {
        get => Factory!.Exists ? Factory.ModelId : string.Empty;
        set
        {
            if (Factory!.Exists) Factory.ModelId = value;
        }
    }
    public string SerialNumber
    {
        get => Factory!.Exists ? Factory.SerialNumber.ToString() : string.Empty;
        set
        {
            if (Factory!.Exists) Factory.SerialNumber = new Base34(value);
        }
    }
    public bool EnableFactoryCFG => Factory is not null and { Exists: true };
    public FactoryCFG? Factory
    {
        get => factory; set
        {
            factory = value;
            try
            {
                factory?.ReadFactory();
            }
            catch (System.Exception)
            {

            }
        }
    }
    public ObservableCollection<Package>? Packages { get; set; }
}
