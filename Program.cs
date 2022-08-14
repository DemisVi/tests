var arr = Array.Empty<HelloWorld>();

var ne = new HelloWorld() { Num = 2, Pum = 5 };
var arr2 = arr.Append(ne);

System.Console.WriteLine(arr2.Count());
foreach (var i in arr2)
    System.Console.WriteLine(i);

public class HelloWorld
{
    public int Num { get; set; } = 0;
    public int Pum { get; set; } = 1;

    public override string ToString() => $"Num: {Num} Pum: {Pum}";
}