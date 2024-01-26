using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using PackageManager.Models;
using PackageManager.Services;

namespace PackageManager.ViewModels;

public class MainPageViewModel : ViewModelBase
{
    public MainPageViewModel()
    {
        var fwProvider = new FirmwareProvider();
        var snProvider = new SourceNodeProvider();

        FirmwareSources = new(new FirmwareSourcesProvider().GetSources());

        FirmwareCollection = new(FirmwareSources.Select(x => fwProvider.GetFirmware(x)));

        SourceNodes = new(snProvider.GetSourceNodes());
    }

    public ObservableCollection<FirmwareSource>? FirmwareSources { get; set; }
    public ObservableCollection<IEnumerable<Firmware>>? FirmwareCollection { get; set; }
    public ObservableCollection<SourceNode>? SourceNodes { get; set; }

    public void CreateDummySources()
    {
        var words = new string[] { "Granta", "Vestaa", "Larges", "Haval", "Niva", "GAZ", "PAZ", "ZAZ" };

        foreach (var i in FirmwareSourcesProvider.DefaultSources)
        {
            var dir = Path.Combine(Environment.CurrentDirectory, i.SubfolderName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (var j in words)
            {
                var subdir = Path.Combine(dir, "848" + Random.Shared.Next(1001, 9009).ToString() + "_" + j);
                if (!Directory.Exists(subdir))
                    Directory.CreateDirectory(subdir);

                var verCount = Random.Shared.Next(1, 7);

                for (var n = 0; n < verCount; n++)
                    Directory.CreateDirectory(Path.Combine(subdir, GetRandomVersionName(Path.GetFileName(subdir))));
            }

        }
    }

    public static string GetRandomVersionName(string? name)
    {
        var s = $"S-0{Random.Shared.Next(99):D2}";
        var r = $"r{Random.Shared.Next(99):D2}";

        return "packet " + s + (!string.IsNullOrEmpty(name) ? $"_{name}_" : "_") + r;
    }
}
