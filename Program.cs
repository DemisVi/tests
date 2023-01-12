using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

System.Console.WriteLine("L000".ToInt32());
/*
#pragma warning disable CA1416

WqlEventQuery creationEventQuery = new WqlEventQuery(
    "SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE " +
    "TargetInstance ISA 'Win32_PnPEntity' " +
    "AND TargetInstance.Caption LIKE '%Telit Serial Diagnostics Interface%'");

WqlEventQuery delevionEventQuery = new WqlEventQuery(
    "SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE " +
    "TargetInstance ISA 'Win32_PnPEntity' " +
    "AND TargetInstance.Caption LIKE '%Telit Serial Diagnostics Interface%'");

WqlObjectQuery deviceQuery = new WqlObjectQuery(
    "SELECT * FROM Win32_PnPEntity WHERE " +
    "Caption LIKE '%Telit Serial Diagnostics Interface%'");

string WaitForEvent(WqlEventQuery query)
{
    using var watcher = new ManagementEventWatcher(query);
    var deviceMOFtext = watcher.WaitForNextEvent().GetText(TextFormat.Mof);
    watcher.Stop();     // WMI EventWatcher need to be softly stoped
    var portName = deviceMOFtext.Split(new char[] { '(', ')' })
                                .Where(x => x.Contains("COM", StringComparison.OrdinalIgnoreCase)).First();
    return portName;
}

string GetDevice(WqlObjectQuery query)
{
    using var searcher = new ManagementObjectSearcher(query);
    var objCollection = searcher.Get();
    if (objCollection.Count <= 0) return string.Empty;
    var deviceMOFtext = objCollection.Cast<ManagementObject>().First().GetText(TextFormat.Mof);

    var portName = deviceMOFtext.Split(new char[] { '(', ')' })
                                .Where(x => x.Contains("COM", StringComparison.OrdinalIgnoreCase)).First();
    return portName;
}

#pragma warning restore CA1416

// Testing
while (true)
{
    System.Console.WriteLine(GetDevice(deviceQuery));
    await Task.Delay(100);
}


var _lock = new object();

var modemPort = new SerialPort("COM5")
{
    BaudRate = 115200,
    // Handshake = Handshake.RequestToSend,
    DtrEnable = true,
    StopBits = StopBits.One,
    Parity = Parity.None,
    // RtsEnable = true,
    // NewLine = "\0",
};
var virtualCom = new SerialPort("COM111")
{
    BaudRate = 115200,
    // Handshake = Handshake.RequestToSend,
    DtrEnable = true,
    StopBits = StopBits.One,
    Parity = Parity.None,
    // RtsEnable = true,
    // NewLine = "\0",
};

modemPort.Open();
virtualCom.Open();

modemPort.DataReceived += (s, _) => ProcessData(s, virtualCom, nameof(modemPort));
virtualCom.DataReceived += (s, _) => ProcessData(s, modemPort, nameof(virtualCom));

modemPort.ErrorReceived += (_, e) => System.Console.WriteLine(e.EventType);
virtualCom.ErrorReceived += (_, e) => System.Console.WriteLine(e.EventType);

void ProcessData(object obj, SerialPort opositePort, string portName)
{
    lock (_lock)
    {
        var port = obj as SerialPort;
        var buff = new byte[port.BytesToRead];
        // System.Console.Write($"{portName} received: {port?.BaseStream.Read(buff, 0, buff.Length)} bytes. {{{string.Join(", ", buff.Select(x => string.Format($"0x{x:X2}")))}}}");
        opositePort.BaseStream.Write(buff, 0, buff.Length);
        // System.Console.WriteLine();
    }
}

await Task.Delay(Timeout.Infinite);

void GoDirectory()
{
    var data = new Dictionary<string, List<string>>();

    var datadir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    System.Console.WriteLine($"{datadir}, {Directory.Exists(datadir)}");
    var subdirs = new DirectoryInfo(datadir).GetDirectories();
    foreach (var i in subdirs)
    {
        data.Add(i.Name, new List<string>());
        foreach (var sd in i.GetDirectories())
        {
            data[$"{i.Name}"].Add(sd.Name);
        }
    }

    foreach (var i in data.Keys)
        System.Console.WriteLine(i);

    foreach (var i in data.Values)
    {
        System.Console.WriteLine(i.GetType().Name);
        foreach (var j in i)
            System.Console.WriteLine(j);
    }
}

*/

public class FactoryCFG
{
    private const string _baseName = "factory.cfg";
    private readonly string _path;
    private Dictionary<string, string> _factory = new();
    public string ModelId => this["MODEL_ID"];
    public Base34 SerialNumber
    {
        get => this["SERIAL_NUMBER"].ToBase34();
        set => this["SERIAL_NUMBER"] = value.ToDeviceSerial();
    }

    public FactoryCFG(string path = "./") => _path = Path.Combine(path, _baseName);

    public void ReadFactory()
    {
        var lines = File.ReadAllLines(_path);
        foreach (var l in lines)
        {
            var temp = l.Split('=');
            _factory.Add(temp.First(), temp.Last());
        }
    }

    public void SaveFactory()
    {
        var lines = _factory.Select(x => string.Join('=', x.Key, x.Value)).ToArray();
        File.WriteAllLines(_path, lines);
    }

    public string this[string parameter]
    {
        get => _factory[parameter];
        set => _factory[parameter] = value;
    }
}

public class Base34
{
    private int _value = 0;
    private static readonly char[] base34 = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public static char[] Base => base34;
    public Base34(string value) => _value = value.ToInt32();
    public Base34(int value) => _value = value;
    public override string ToString() => _value.ToBase34();
    public static Base34 operator ++(Base34 item)
    {
        item._value++;
        return item;
    }

    public static Base34 operator --(Base34 item)
    {
        item._value--;
        return item;
    }
    public static explicit operator string(Base34 base34) => base34.ToString();
    public static explicit operator Base34(string base34String) => new Base34(base34String);
}

public static class Base34Extensions
{
    public static string ToDeviceSerial(this Base34 value) =>
        $"T7G84178{DateTime.Now.DayOfYear.ToString("D3")}{DateTime.Now.ToString("yy")}0000{value.ToString()}";

    public static Base34 ToBase34(this string value)
    {
        if (value.Contains("___")) return new Base34(string.Empty);
        if (!value.StartsWith("T7G84178", true, null) && !string.IsNullOrEmpty(value)) throw new ArgumentException("String is not in DeviceSerial format");
        if (value.Contains("000", StringComparison.Ordinal))
            return new Base34(value.Substring(value.IndexOf("000", StringComparison.Ordinal)));
        else return new Base34(string.Empty);
    }
    public static int ToInt32(this char value) => ToInt32(value.ToString());
    public static int ToInt32(this string value)
    {
        var serial = value.ToUpper().Reverse();
        int integer = 0;
        int power = 1;

        foreach (var c in serial)
        {
            int index = Array.IndexOf(Base34.Base, c);

            if (index == -1) throw new ArgumentException("Not a base-34 string");

            integer += index * power;
            power *= 34;
        }
        return integer;
    }

    public static string ToBase34(this int value, int rank = 4)
    {
        StringBuilder result = new();
        int targetBase = (int)Base34.Base.Length;

        do
        {
            result.Insert(0, Base34.Base[value % targetBase]);
            value = value / targetBase;
        }
        while (value > 0);

        if (rank > 0) result.Insert(0, "0", rank - result.Length);

        return result.ToString();
    }
}
