using System;
using System.Collections.Generic;
using ArteryISPProg.Models;

namespace tests.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        var row = new MemoryRow();
        var data = new byte[16];
        row.Address = Constants.StartAddress;
        Random.Shared.NextBytes(data);
        row.Set(data);
        Items.Add(row);
    }
    public List<MemoryRow> Items { get; set; } = new();
}
