using System;
using System.Reflection;
using System.Text.Json;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.IO.Ports;
using System.Timers;
using IntelHexFormatReader;
using FTD2XX_NET;
using Wrench.Services;

Adapter a = new("USBCOM17A");
a.OpenAdapter();
a.BeginWatchSensorState();

a.SensorStateChanged += (_, e) =>
    System.Console.WriteLine(e.AdapterState);

await Task.Delay(Timeout.Infinite);