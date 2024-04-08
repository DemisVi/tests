using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using PackageManager.Models;
using PackageManager.DataTypes;
using ReactiveUI;
using Avalonia.Platform.Storage;
using System.Linq;

namespace PackageManager.ViewModels;

public class FirmwareViewModel : ViewModelBase
{
    private Firmware firmware;

    public FirmwareViewModel(Firmware fw)
    {
        firmware = fw;
        ModelId = fw.ModelId;
        SerialNumber = fw.SerialNumber;

        RemovePackage = ReactiveCommand.Create<Package>(PerformRemovePackage);
    }

    public ReactiveCommand<Package, Unit> RemovePackage { get; }

    public Firmware Firmware { get => firmware; set => this.RaiseAndSetIfChanged(ref firmware, value); }
    public string ModelId { get; set; }
    public string SerialNumber { get; set; }

    public void Save()
    {
        Firmware.ModelId = ModelId;
        Firmware.SerialNumber = SerialNumber;
        Firmware.Factory?.SaveFactory();
    }

    public void AddPackage(IEnumerable<IStorageFolder> dirs)
    {
        foreach (var dir in dirs)
        {
            var targetDir = Path.Combine(Firmware.FirmwarePath, dir.Name);

            Directory.CreateDirectory(targetDir);

            foreach (var f in Directory.EnumerateFiles(dir.Path.LocalPath))
            {
                File.Copy(f, f.Replace(Path.GetDirectoryName(f) ?? "", targetDir), true);
            }

            Firmware.Packages?.Add(new Package()
            {
                ModelName = Firmware.ModelName,
                PackagePath = targetDir,
                VersionName = new DirectoryInfo(targetDir).Name,
            });
        }
    }

    public void PerformRemovePackage(Package pack)
    {
        try
        {
            Directory.Delete(pack.PackagePath, true);
            Firmware.Packages?.Remove(pack);
        }
        catch (DirectoryNotFoundException) { }
    }
    public void PerformArchivePackage(Package pack)
    {
        var archiveDir = Path.Combine(Environment.CurrentDirectory, Constants.ArchiveName);
        var fwName = Path.GetFileName(Path.GetDirectoryName(pack.PackagePath)) ?? throw new DirectoryNotFoundException();
        var packageName = Path.GetFileName(pack.PackagePath);
        var targetFwDir = Path.Combine(archiveDir, fwName, packageName);

        var packageFiles = Directory.GetFiles(pack.PackagePath);

        if (!Directory.Exists(targetFwDir)) Directory.CreateDirectory(targetFwDir);

        try
        {
            foreach (var f in packageFiles)
            {
                var d = f.Replace(pack.PackagePath, targetFwDir);
                File.Copy(f, d, true);
            }

            Directory.Delete(pack.PackagePath, true);
            Firmware.Packages?.Remove(pack);
        }
        catch (ArgumentNullException) { }
        catch (DirectoryNotFoundException) { }
    }
}
