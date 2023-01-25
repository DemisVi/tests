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
using FTD2XX_NET;
using Wrench.Model;

var ft = new FTDI();


var cu1 = new ContactUnit("CANCOM26", "COM49", 200D, System.Console.WriteLine);

Task.Factory.StartNew(() => Test(cu1));

Thread.Sleep(Timeout.Infinite);

void Test(ContactUnit cu)
{
    int up = 0, down = 0, fail = 0, jamm = 0;
    while (true)
    {
        cu.SetOuts(Outs.Pn1 | Outs.Cyan);
        // Console.ReadKey();
        if (cu.WaitForBits(Sensors.Pn1_Up, 5) != Sensors.None)
        {
            up++;
        }
        else fail++;
        cu.SetOuts(Outs.None | Outs.Magenta);
        if (cu.WaitForBits(Sensors.Pn1_Down, 5) != Sensors.None)
        {
            down++;
        }
        else jamm++;

        // lock (_lock)
        {
            System.Console.WriteLine("{0}> up: {1} / down: {4} / fail: {2} / jamm: {3}", cu.PortName, up, fail, jamm, down);
        }
    }
}