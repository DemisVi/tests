using System;
using System.IO.Ports;
using System.Reflection.Metadata;
using Iot.Device.Mcp23xxx;
using Microsoft.Win32.SafeHandles;
using OfficeOpenXml.Packaging.Ionic.Zip;

public static class SerialPortExtensions
{
    /// <summary>
    /// Send AT command to Modem Port and try to read ansver.
    /// </summary>
    /// <param name="port">Modem SerialPort</param>
    /// <param name="request">AT string to send</param>
    /// <param name="responce">Once read answer success, will contain response, else exception message.</param>
    /// <param name="millisecondsTimeout">Time before read response message</param>
    /// <returns>
    /// "true" if request is success, "false" if port is died.
    /// Also not mean that request is unsuccessful.
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool TryGetResponse(this SerialPort port, string request, out string responce, int millisecondsTimeout = 200)
    {
        if (port is { IsOpen: false }) throw new InvalidOperationException("the port is closed");

        try
        {
            port.WriteLine(request);

            Thread.Sleep(millisecondsTimeout);

            responce = port.ReadExisting();
            return true;
        }
        catch (InvalidOperationException ex)
        {
            port.KillHandle();

            responce = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// Close internal SerialStream handle
    /// </summary>
    /// <param name="port"></param>
    public static void KillHandle(this SerialPort port)
    {
        var stream = typeof(SerialPort).GetField("_internalSerialStream", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(port);
        var handle = (SafeFileHandle?)stream?.GetType().GetField("_handle", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(stream);

        if (handle is not null and { IsClosed: false })
            handle.Close();
    }
}
