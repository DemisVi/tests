using System;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.IO.Ports;
using System.Timers;

#pragma warning disable CA1416 // Disable platform compatibility warning

using sole = System.Console;

WqlEventQuery eventQueryTelit = new WqlEventQuery(
                    "SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE " +
                    "TargetInstance ISA 'Win32_POTSModem'" +
                    "AND TargetInstance.Caption LIKE '%telit%'" +
                    " GROUP WITHIN 10");

WqlEventQuery eventQueryDevice = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent" +
                    " WHERE EventType = 2 GROUP WITHIN 4");

ObjectQuery queryTelitModem = new ObjectQuery("SELECT AttachedTo FROM Win32_POTSModem WHERE" +
                    " Caption LIKE '%telit%'");

ManagementEventWatcher watcher = new ManagementEventWatcher(eventQueryTelit);
Console.WriteLine("Waiting for an event...");

watcher.EventArrived += new EventArrivedEventHandler(ResolveEvent);

watcher.Start();

Task.Run(() =>
{
    while (true)
    {
        watcher.WaitForNextEvent();
        System.Console.WriteLine("event triggered!");
    }
});

Thread.Sleep(Timeout.Infinite);

watcher.Stop();

void ResolveEvent(object? obj, EventArrivedEventArgs e)
{
    sole.WriteLine("======================" + DateTime.Now.ToString("HH:mm:ss") + "======================");

    using var searcher = new ManagementObjectSearcher(queryTelitModem); // WHERE Caption LIKE '%telit%' AND Status = 'OK'

    foreach (ManagementObject queryObj in searcher.Get())
    {
        System.Console.WriteLine(queryObj["AttachedTo"]);
    }
}
