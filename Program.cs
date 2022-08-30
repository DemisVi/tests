using System;
using System.Text;
using System.Security.Cryptography;

using var sha1 = SHA1.Create();

var hash = sha1.ComputeHash(Encoding.Unicode.GetBytes("12345"));

foreach (var i in hash)
    System.Console.Write("{0,-4:X2}", i);