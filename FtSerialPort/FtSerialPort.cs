using System;
using System.Linq;
using System.IO.Ports;
using System.Management;

#pragma warning disable CA1416

public class FtSerialPort : SerialPort
{
    public new int BaudRate { get; } = 115200;
    public FtSerialPort(string portName) : base(portName) => base.BaudRate = BaudRate;

    public new static string[] GetPortNames()
    {
        using var searcher = new ManagementObjectSearcher(FtdiQueries.FtdiSerialDevice);
        var objs = searcher.Get();

        if (objs is { Count: > 0 })
            return objs.Cast<ManagementObject>().Select(x => GetSerialName(x)).ToArray();
        else
            return Array.Empty<string>();
    }

    private static string GetSerialName(ManagementObject x) => x.GetText(TextFormat.Mof).Split(
        new char[] { '(', ')' },
        StringSplitOptions.RemoveEmptyEntries).First(x => x.StartsWith("COM"));
}

#pragma warning restore CA1416
