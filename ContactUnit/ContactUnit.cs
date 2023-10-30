using System;
using System.Text;
using System.Linq;
using System.IO.Ports;
using System.Management;
using System.Device.Gpio;
using Iot.Device.Ft2232H;
using Iot.Device.FtCommon;
using Wrench.Model;

public class ContactUnit : IContactUnit, IDisposable
{
    private GpioOutputs _outputs;
    private bool disposedValue;

    private Ftx232HDevice CuFtDevice { get; }
    private FtSerialPort CuSerialPort { get; }
    private GpioPin PowerPin { get; }
    public GpioOutputs Outputs
    {
        get => _outputs;
        private set
        {
            _outputs = value;
            WriteGpio();
        }
    }
    public GpioInputs Inputs { get; private set; }

    public ContactUnit(FtSerialPort cuSerialPort, Ftx232HDevice ftDevice)
    {
        CuSerialPort = cuSerialPort;
        CuFtDevice = ftDevice;
        using var gpio = CuFtDevice.CreateGpioController();
        PowerPin = gpio.OpenPin(Ft2232HDevice.GetPinNumberFromString("ADBUS0"), PinMode.Output);
        CuSerialPort.Open();
    }

    public void LockBoard() => Outputs |= GpioOutputs.Pn1;

    public void ReleaseBoard() => Outputs &= ~GpioOutputs.Pn1;

    public void PowerOffBoard() => PowerPin.Write(PinValue.High);

    public void PowerOnBoard() => PowerPin.Write(PinValue.Low);

    public void LEDOff() => Outputs &= ~GpioOutputs.White;

    public void LEDBlue()
    {
        LEDOff();
        Outputs |= GpioOutputs.Blue;
    }

    public void LEDGreen()
    {
        LEDOff();
        Outputs |= GpioOutputs.Green;
    }

    public void LEDRed()
    {
        LEDOff();
        Outputs |= GpioOutputs.Red;
    }

    public void LEDYellow()
    {
        LEDOff();
        Outputs |= GpioOutputs.Yellow;
    }

    public void LEDCyan()
    {
        LEDOff();
        Outputs |= GpioOutputs.Cyan;
    }

    public void LEDMagenta()
    {
        LEDOff();
        Outputs |= GpioOutputs.Magenta;
    }

    public void LEDWhite()
    {
        LEDOff();
        Outputs |= GpioOutputs.White;
    }

    private void WriteGpio()
    {
        CuSerialPort.Write(CuCommands.CuWriteOutputs, 0, CuCommands.CuWriteOutputs.Length);
        CuSerialPort.Write(new byte[] { (byte)Outputs }, 0, 1);
    }

    private void ReadGpio()
    {
        CuSerialPort.Write(CuCommands.CuReadInputs, 0, CuCommands.CuReadInputs.Length);

        var received = new byte[3];
        CuSerialPort.Read(received, 0, received.Length);
        Inputs = (GpioInputs)received[2];
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                CuSerialPort.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~ContactUnit()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
