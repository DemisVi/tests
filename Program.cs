using System;
using System.IO;
using System.Security.Cryptography;

var source1 = args[0];
var source2 = args[1];

var files1 = Directory.EnumerateFiles(args[0], "*", SearchOption.AllDirectories);
var files2 = Directory.EnumerateFiles(args[1], "*", SearchOption.AllDirectories);

var dict1 = new Dictionary<string, byte[]>();
var dict2 = new Dictionary<string, byte[]>();

Calculate(ref dict1, files1);
Calculate(ref dict2, files2);

var compared = Compare(dict1, dict2);

System.Console.WriteLine($"arg1 = {dict1.Count} files. arg2 = {dict2.Count}\nCompared {compared} files with no differences");

int Compare(Dictionary<string, byte[]> left, Dictionary<string, byte[]> right)
{
    Dictionary<string, byte[]> greaterOne;
    Dictionary<string, byte[]> lesserOne;
    var count = 0;

    if (left.Count >= right.Count)
    {
        greaterOne = left;
        lesserOne = right;
    }
    else
    {
        greaterOne = right;
        source1 = args[1];
        lesserOne = left;
        source2 = args[0];
    }

    foreach (var key1 in greaterOne.Keys)
    {
        var key2 = key1.Replace(source1, source2);
        try
        {
            if (greaterOne[key1].SequenceEqual(lesserOne[key2]))
            {
                count++;
                //Console.WriteLine("{0}, {1}, {2}", key, Convert.ToHexString(greaterOne[key]), "OK");
            }
            else
            {
                Console.WriteLine("{0},\t{1}\n{2},\t{3}", key1, key2, Convert.ToHexString(greaterOne[key1]), Convert.ToHexString(lesserOne[key2]));
            }
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    return count;
}

void Calculate(ref Dictionary<string, byte[]> dict, IEnumerable<string> files)
{
    using var sha256 = SHA1.Create();
    foreach (var file in files)
        // System.Console.WriteLine(file);
        dict.Add(file, sha256.ComputeHash(File.ReadAllBytes(file)));
}
