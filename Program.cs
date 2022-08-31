using System;
using System.Reflection;

C c = new();

var attr = c.GetType().GetCustomAttributes();

foreach (var i in attr)
{
    System.Console.WriteLine(i.GetType().Name);
}

[MyAttrib("123123123"), Serializable]
public class C
{
    private int I { get; set; }
    public int PI { get; set; }

    private int fI = 0;
    public int pfI = 0;
}

public class MyAttrib : Attribute
{
    public MyAttrib(string name) => Name = name;
    public string Name { get; set; }
}