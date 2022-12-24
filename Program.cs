using System;
using System.Text;
using System.Linq;

var fac = new FactoryCFG("C:\\Users\\lyuds\\Code\\test\\");
fac.ReadFactory();
System.Console.WriteLine(fac["serial_number"]);

public class FactoryCFG
{
    private const string _baseName = "factory.cfg";
    private readonly string _path;

    private Dictionary<string, string> _factory = new();

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

    public string this[string parameter] => _factory[parameter];
}

public class Base34
{
    private static readonly char[] base34 = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    public static char[] Base => base34;
}
public static class Base34Extensions
{
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

    public static string ToBase34(this int value, int rank = 0)
    {
        StringBuilder result = new();
        int targetBase = (int)Base34.Base.Length;

        do
        {
            result.Insert(0, Base34.Base[value % targetBase]);
            value = value / targetBase;
        }
        while (value > 0);

        if (rank > 0) result.Insert(0, "0", rank);

        return result.ToString();
    }
}
