using System;
using System.Globalization;
using System.IO;
using System.Text;
using Wrench.Model;
using Wrench.Services;

if (true)
{
    var file = File.ReadAllLines("./log.log");
    for (var i = 0; i < file.Length; i++)
    {
        for (var j = i + 1; j < file.Length; j++)
        {
            if (file[i] == file[j])
                System.Console.WriteLine(file[i]);
        }
    }
}

if (false)
{
    var adapter = new AdapterLocator().AdapterSerials.Where(x => x.EndsWith("A")).First().Trim('A');
    var unit = ContactUnit.GetInstance(adapter);
    var factory = new FactoryCFG();
    var adb = new Adb();
    int max = "ZZZZ".ToInt32();
    var log = File.AppendText("./log.log");

    do
    {
        unit.PowerOn();
        Thread.Sleep(TimeSpan.FromSeconds(12));
        adb.Run("shell ls -l /data/factory.cfg");
        Console.WriteLine(adb.LastStdOut);
        adb.Run("shell cat /data/factory.cfg");
        Console.WriteLine(adb.LastStdOut);
        var text = adb.LastStdOut?
            .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.StartsWith("SERIAL"))
            .First();
        log.WriteLine(text);
        log.Flush();
        factory.ModelId = Random.Shared.Next(1, 20).ToString();
        factory.SerialNumber = new Base34(Random.Shared.Next(max));
        factory.SaveFactory();
        adb.Run("push \"./factory.cfg\" /data");
        Console.WriteLine(adb.LastStdOut);
        adb.Run("shell ls -l /data/factory.cfg");
        Console.WriteLine(adb.LastStdOut);
        adb.Run("shell cat /data/factory.cfg");
        Console.WriteLine(adb.LastStdOut);
        adb.Run("shell sync");
        adb.Run("reboot");
        Thread.Sleep(TimeSpan.FromSeconds(5));
        unit.PowerOff();
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }
    while (true);



}