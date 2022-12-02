using System;
using System.Text.Json;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.IO.Ports;
using System.Timers;

#pragma warning disable CA1416 // Disable platform compatibility warning

WqlEventQuery queryEventTelit = new WqlEventQuery(
                    "SELECT * FROM __InstanceCreationEvent " + "WITHIN 1 WHERE " +
                    "TargetInstance ISA 'Win32_POTSModem' " +
                    "AND (TargetInstance.Caption LIKE '%Telit%' " +
                    "OR TargetInstance.ProviderName LIKE '%Telit%' " +
                    "OR TargetInstance.DeviceID LIKE '%VID_1BC7&PID_1201%') " +
                    "GROUP WITHIN 4");

WqlEventQuery queryEventDevice = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent" +
                    " WHERE EventType = 2 GROUP WITHIN 4");

ObjectQuery queryTelitModem = new WqlObjectQuery("SELECT AttachedTo FROM Win32_POTSModem WHERE" +
                    " DeviceID LIKE '%VID_1BC7&PID_1201%'");

ManagementEventWatcher watcher1 = new ManagementEventWatcher(queryEventTelit);
Console.WriteLine("Waiting for an event...");

int eventCount = 0;

watcher1.EventArrived += new EventArrivedEventHandler(ResolveEvent);

watcher1.Start();

Console.CancelKeyPress += (_, _) =>
{
    watcher1.Stop();
};

Thread.Sleep(Timeout.Infinite);

void ResolveEvent(object? obj, EventArrivedEventArgs e)
{
    Console.WriteLine($"{eventCount++}: " + DateTime.Now.ToString("HH:mm:ss") + " >");

    using var searcher = new ManagementObjectSearcher(queryTelitModem);

    foreach (ManagementObject queryObj in searcher.Get())
        System.Console.WriteLine(queryObj["AttachedTo"]);
}
