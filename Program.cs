using System;
using System.Text;
using System.Linq;

var fac = new FactoryCFG("./");
fac.ReadFactory();
while (true)
{
    fac.SerialNumber++;
    fac.SaveFactory();
    Thread.Sleep(1000);
}

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
        if (!value.StartsWith("T7G84178") && !string.IsNullOrEmpty(value)) throw new ArgumentException("String is not in DeviceSerial format");
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
