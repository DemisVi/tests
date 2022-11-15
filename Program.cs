using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using FTD2XX_NET;

var ft = new FTDI();

uint _devCount = 0;
FTDI.FT_DEVICE_INFO_NODE[] _ftdiDeviceList;

ft.GetNumberOfDevices(ref _devCount);
_ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[_devCount];
ft.GetDeviceList(_ftdiDeviceList);

Console.WriteLine(_devCount);

foreach (var i in _ftdiDeviceList.Select(x => x.SerialNumber).ToList())
    Console.WriteLine(i);