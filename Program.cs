using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

try
{
    new ConverterTests().Basic_Tests();
}
catch (System.Exception ex)
{
    System.Console.WriteLine(ex.Message);
}

[TestFixture]
public class ConverterTests
{
    [Test]
    public void Basic_Tests()
    {
        Assert.AreEqual("", Kata.BinaryToString(""));
        Assert.AreEqual("Hello", Kata.BinaryToString("0100100001100101011011000110110001101111"));
    }
}

public static class Kata
{
    public static string BinaryToString(string binary)
    {
        if (string.IsNullOrEmpty(binary)) return string.Empty;

        var sb = new StringBuilder();
        var stringLength = binary.Length % 8 == 0 ? binary.Length / 8 : binary.Length / 8 + 1;
        var bytes = new byte[stringLength];
        var counter = 0;

        foreach (var i in binary.Chunk(8))
        {
            var tempString = new string(i);
            bytes[counter++] = Convert.ToByte(tempString, 2);
        }
    
        sb.Append(Encoding.ASCII.GetChars(bytes));

        return sb.ToString();
    }
}