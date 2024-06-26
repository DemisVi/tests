using System;
using System.Security;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using PackageManager.Models;
using PackageManager.DataTypes;
using Avalonia;

namespace PackageManager.Services;

public class FirmwareSourcesProvider
{
    public static string DefaultFileName { get; } = "Sources.json";
    public string DefaultSourcesPath { get; set; } = Path.Combine(Environment.CurrentDirectory, DefaultFileName);
    public static IEnumerable<FirmwareSource> DefaultSources { get; } = new FirmwareSource[] {
         new()
         {
            Name = "SimCom ПФ",
            SubfolderName = "./SimCom_full",
            DeviceType = DeviceType.SimComFull,
        },
        new() {
            Name = "SimCom Ретрофит",
            SubfolderName = "./SimCom_retro",
            DeviceType = DeviceType.SimComRetro,
        },
        new() {
            Name = "SimCom Упрощенный",
            SubfolderName = "./SimCom_simple",
            DeviceType = DeviceType.SimComSimple,
        },
        new() {
            Name = "Telit Ретрофит",
            SubfolderName = "./Telit_retro",
            DeviceType = DeviceType.TelitRetro,
        },
        new() {
            Name = "Telit Упрощенный",
            SubfolderName = "./Telit_simple",
            DeviceType = DeviceType.TelitSimple,
        },
        new() {
            Name = "SimCom Технолабс",
            SubfolderName = "./SimCom_techno",
            DeviceType = DeviceType.SimComTechno,
        }};

    public IEnumerable<FirmwareSource> GetSources() => GetSources(DefaultSourcesPath);
    public IEnumerable<FirmwareSource> GetSources(DirectoryInfo directory) => GetSources(Path.Combine(directory.FullName, DefaultFileName));
    public IEnumerable<FirmwareSource> GetSources(string filePath)
    {
        if (File.Exists(filePath) is not true)
        {
            foreach (var i in DefaultSources)
                i.SubfolderName = Path.Combine(Path.GetDirectoryName(filePath)!, i.SubfolderName);
            var @default = JsonSerializer.Serialize(DefaultSources);
            File.WriteAllText(filePath, @default);
            return GetSources(filePath);
        }
        else
        {
            try
            {
                var fwProvider = new FirmwareProvider();
                var src = JsonSerializer.Deserialize<FirmwareSource[]>(File.ReadAllText(filePath));
                foreach (var i in src!)
                {
                    i.Firmware = new(fwProvider.GetFirmware(i));
                    var dir = Path.GetDirectoryName(filePath);
                    i.SubfolderName = Path.Combine(dir!, i.SubfolderName);
                }

                return src;
            }
            catch (JsonException ex)
            {
                return new FirmwareSource[] { new() { Name = ex.Message } };
            }
        }
    }

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
};
