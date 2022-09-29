using System;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

try
{
    new SolutionTest().SampleTest();
}
catch (System.Exception ex)
{
    System.Console.WriteLine(ex.Message);
}

public static class Kata
{
    public static string AlphabetPosition(string text)
    {
        var chars = new List<int>();

        foreach (var c in text)
            chars.Add(char.IsLetter(c) ? char.IsLower(c) ? (c - '`') : (c - '@') : 0);

        return string.Join(' ', chars.Where(x => x != 0));
    }
}

[TestFixture]
public class SolutionTest
{
    [Test]
    public void SampleTest()
    {
        Assert.AreEqual("20 8 5 19 21 14 19 5 20 19 5 20 19 1 20 20 23 5 12 22 5 15 3 12 15 3 11", Kata.AlphabetPosition("The sunset sets at twelve o' clock."));
        Assert.AreEqual("20 8 5 14 1 18 23 8 1 12 2 1 3 15 14 19 1 20 13 9 4 14 9 7 8 20", Kata.AlphabetPosition("The narwhal bacons at midnight."));
    }
}