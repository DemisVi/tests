using System;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

#if DEBUG
System.Console.WriteLine("DEBUGGGGGG");
#elif RELEASE
System.Console.WriteLine("RELEEEEEASE");
#endif


/*
var sourcePath = Path.Combine(Environment.CurrentDirectory, "./Data/");
var destPath = "C:/Wrench/Data/";
var factoryPath = Directory.GetFiles(sourcePath, "*.cfg", SearchOption.AllDirectories).First();
var factoryDir = Path.GetDirectoryName(factoryPath);
var factoryName = Path.GetFileName(factoryPath);

UpdateCFG(factoryDir);

CopyPackage(sourcePath, destPath);



void CopyPackage(string? source, string? dest)
{
    if (string.IsNullOrEmpty(source)) throw new FileNotFoundException("could not copy from nowhere");
    if (string.IsNullOrEmpty(dest)) throw new FileNotFoundException("could not copy to nowhere");
    var dirs = Directory.GetDirectories(source, "*", SearchOption.AllDirectories);
    var files = Directory.GetFiles(source, "*", SearchOption.AllDirectories);

    foreach (var dir in dirs) Directory.CreateDirectory(dir.Replace(source, dest));
    foreach (var fi in files) File.Copy(fi, fi.Replace(source, dest));
}

void UpdateCFG(string? path)
{
    if (string.IsNullOrEmpty(path)) throw new FileNotFoundException("factory.cfg path not found");

    var fac = new FactoryCFG(path);
    fac.ReadFactory();

    fac.SerialNumber += (Base34)"1000";

    System.Console.WriteLine($"Serial: {fac.SerialNumber}");

    fac.SaveFactory();
}
*/