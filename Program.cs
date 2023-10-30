using System;
using Wrench.Model;

var searcher = new SerialPortSearcher();

searcher.Query = WqlQueries.ObjectSimcomModem;
System.Console.WriteLine(searcher.GetPortNames().FirstOrDefault());
searcher.Query = WqlQueries.ObjectSimcomATPort;
System.Console.WriteLine(searcher.GetPortNames().FirstOrDefault());
searcher.Query = WqlQueries.ObjectFtdiSerial;
System.Console.WriteLine(searcher.GetPortNames().FirstOrDefault());
