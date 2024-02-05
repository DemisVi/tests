using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PackageManager.Models;

namespace PackageManager.Services;

public class FirmwareProvider
{
    public FirmwareSource? Source { get; set; }
    public FirmwareProvider() { }
    public FirmwareProvider(FirmwareSource source)
    {
        Source = source;
    }

    public IEnumerable<Firmware> GetFirmware() => GetFirmware(Source);
    public IEnumerable<Firmware> GetFirmware(FirmwareSource? source)
    {
        if (source is null) throw new ArgumentException("Source can't be null or empty", nameof(source));

        var path = Path.Combine(Environment.CurrentDirectory, source.SubfolderName);

        if (Directory.Exists(path) is not true)
            return Enumerable.Empty<Firmware>();

        var firmwareDirectories = Directory.GetDirectories(path);
        return firmwareDirectories.Select(dir => new Firmware()
        {
            FirmwarePath = dir,
            ModelName = new DirectoryInfo(dir).Name,
            Factory = new FactoryCFG(dir),
            Packages = new(Directory.GetDirectories(dir).Select(indir => new Package()
            {
                ModelName = new DirectoryInfo(dir).Name,
                PackagePath = indir,
                VersionName = new DirectoryInfo(indir).Name,
                DeviceType = source.DeviceType,
            })),
        });
    }
}
