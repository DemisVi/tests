using System;
using System.IO.Ports;
using System.Text;
using System.Management;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ROOT.CIMV2.Win32;

var devs = await new TelitLocator().LocateDevicesAsync();

foreach (var item in devs)
{
    System.Console.WriteLine(item.AttachedTo + " " + item.Serial);
}
