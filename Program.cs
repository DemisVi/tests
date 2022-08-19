using System;

Devs devs = new(new Dev("0 asdf"));
devs.Add(new Dev("1 zxcv"));
devs.AddRange(new[] {new Dev("2 qweqwe"), new("3 zzxcvzxcv"), new("4 zzxcvzxcv")});

foreach (var d in devs)
    System.Console.WriteLine(d);


public class Devs : List<Dev>
{
    public string Name { get; private set; }
    public Devs(string name = "") => Name = name;

    public Devs(Dev dev, string name = "")
    {
        Add(dev);
        Name = name;
    }
}

public class Dev
{
    public string Name { get; private set; } = string.Empty;
    
    public Dev(string name) => Name = name;

    public override string ToString() => Name.ToString();
}