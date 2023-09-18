using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ArteryISPProg.Models;
using Avalonia.ReactiveUI;
using System.Reactive;
using ReactiveUI;
using Avalonia.Threading;
using System.Collections.ObjectModel;

namespace tests.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Task.Factory.StartNew(() =>
        {
            do
            {
                var data = new byte[16];
                Random.Shared.NextBytes(data);
                Items.AddRange(MemoryRow.Generate(data));
                Dispatcher.UIThread.Invoke(new Action(() =>this.RaisePropertyChanged(nameof(Items))));
                Thread.Sleep(1000);
            } while (true);
        });
    }
    public List<MemoryRow> Items { get; set; } = new();
}
