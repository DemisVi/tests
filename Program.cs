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

var path = Environment.GetEnvironmentVariable("PATH")?.Split(';').Select(x => Path.Combine(x, "ls.exe")).Where(x => File.Exists(x)).FirstOrDefault();

System.Console.WriteLine(path);