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

object _lock = new();

Serv();
Thread.Sleep(1000);
Cli();
Thread.Sleep(1000);
Cli();
Thread.Sleep(1000);
Cli();
Thread.Sleep(1000);

async Task Serv()
{
    var buffer = new byte[65536];

    try
    {
        var ep = new IPEndPoint(IPAddress.Any, 123);
        var lis = new TcpListener(ep);
        lis.Start();

        while (true)
        {
            var sock = await lis.AcceptSocketAsync();
            var bytesReceived = sock.Receive(buffer);
            if (bytesReceived > 0)
                System.Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, bytesReceived));
        }
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
}

async Task Cli()
{
    lock (_lock)
    {
        try
        {
            var ep = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 123);
            var cli = new TcpClient();
            cli.Connect(ep);

            cli.GetStream().Write(Encoding.ASCII.GetBytes(DateTime.Now.ToString("G")));
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

    }
}
