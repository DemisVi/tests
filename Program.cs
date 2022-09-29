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
    new KataTests().BasicTests();
}
catch (System.Exception ex)
{
    System.Console.WriteLine(ex.Message);
}

public class Kata
{
    public static int[] SortArray(int[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            if (array[i] % 2 == 0) continue;

            for (var j = i + 1; j < array.Length; j++)
                if (array[j] % 2 != 0)
                    if (array[i] > array[j])
                        Swap(ref array[i], ref array[j]);
        }

        return array;

        void Swap(ref int a, ref int b)
        {
            b ^= a;
            a ^= b;
            b ^= a;
        }
    }
}

[TestFixture]
public class KataTests
{
    [Test]
    public void BasicTests()
    {
        Assert.AreEqual(new int[] { 1, 3, 2, 8, 5, 4 }, Kata.SortArray(new int[] { 5, 3, 2, 8, 1, 4 }));
        Assert.AreEqual(new int[] { 1, 3, 5, 8, 0 }, Kata.SortArray(new int[] { 5, 3, 1, 8, 0 }));
        Assert.AreEqual(new int[] { }, Kata.SortArray(new int[] { }));
    }
}