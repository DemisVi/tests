using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrench.Model;

[Flags]
enum PortBits
{
    KL30 = 1,
    Sensor = 2,
    KL15 = 4,
    Yellow = 8,
    Green = 16,
    Pgm = 32,
    Red = 64
}


[Flags]
public enum ExitCode
{
    OK = 0,
    Fail = -1,
    UserTimeout = 1024,
    WasNotRan = 2048,
}

[Flags]
public enum ErrorCodes
{
    None = 0,
    Lodg = 1,
    Device = 2,
    Pneumo_Up = 4,
    Pneumo_Down = 8,
    Device_Power = 16,
    Device_Attach = 32,
    ADB_Interface = 64,
    Fastboot_Batch = 128,
    ADB_Batch = 256,
    Device_Start = 512,
    Device_AT_config = 1024,

}
