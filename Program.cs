using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

System.Console.WriteLine(BitConverter.ToString(new byte[] {0x01, 0x02, 0xAA}));

public static class DayOfWeekExtension
{
    public static bool IsWeekend(this DayOfWeek day) => (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday);
}