using System;


C c = new();
C d = new();

System.Console.WriteLine(c.Equals(d));
System.Console.WriteLine(c.Equals(null));
System.Console.WriteLine(c?.Equals(c));
System.Console.WriteLine(object.Equals(c, d));

public class C : IEquatable<C>
{
    public char Ch { get; set; } = 'a';

    public bool Equals(C? other)
    {
        if (other == null) return false;

        if (object.ReferenceEquals(this, other)) return true;

        return other.Ch == this.Ch;
    }
}