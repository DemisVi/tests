using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using Avalonia.Threading;
using System.Threading.Tasks;
using PackageManager.Models;
using PackageManager.Extensions;
using ReactiveUI;
using System.Linq;
using DynamicData;
using System.Collections;
using PackageManager.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.Controls;
using Avalonia;
using Avalonia.Platform.Storage;

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

    public /* async */ void PerformRemovePackage(Package pack)
    {
        // var msgBox = MessageBoxManager.GetMessageBoxStandard("Warning", $"Confirm {pack.VersionName} deletion", ButtonEnum.YesNo);
        // var res = await msgBox.ShowAsync();
        // if (pack is not null && res == ButtonResult.Yes)
        {
            Directory.Delete(pack.PackagePath, true);
            Firmware.Packages?.Remove(pack);
        }
    }
}
