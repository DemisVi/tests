using System;
using System.Security.Cryptography;
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
    new Tests().SimpleTest();
}
catch (System.Exception ex)
{
    System.Console.WriteLine(ex.Message);
}


public class CodeWars
{
    public static string crack(string hash)
    {
        using var crypt = MD5.Create();
        hash = hash.ToUpper();

        for (var num = 0; num < 100000; num++)
        {
            var hashBytes = crypt.ComputeHash(Encoding.ASCII.GetBytes(num.ToString("D5")));
            var stringToCompare = string.Join("", hashBytes.Select(x => x.ToString("X2")));

            if (stringToCompare == hash)
            {
                System.Console.WriteLine(num.ToString("D5"));
                return num.ToString("D5");
            }
        }
        return "There is no PIN";
    }
}

[TestFixture]
public class Tests
{
    [Test]
    public void SimpleTest()
    {
        Assert.AreEqual("12345", CodeWars.crack("827ccb0eea8a706c4c34a16891f84e7b"));
        Assert.AreEqual("00078", CodeWars.crack("86aa400b65433b608a9db30070ec60cd"));
    }
}