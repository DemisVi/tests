using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PackageManager.Models;
using PackageManager.Views;
using System.IO;

namespace PackageManager.ViewModels;

public class PackageViewModel : ViewModelBase
{
    public PackageViewModel(Package p)
    {
        Package = p;
        foreach (var i in Directory.EnumerateFiles(p.PackagePath))
            Files.Add(Path.GetFileName(i));
    }

    public Package Package { get; set; } 
    public ObservableCollection<string> Files { get; set; } = new();
}
