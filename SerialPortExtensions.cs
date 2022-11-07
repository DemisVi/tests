using System.IO.Ports;

public static class SerialExtensions
{
    public static async Task ModemStartAsync(this SerialPort port)
    {
        if (!port.IsOpen) throw new("Modem port closed");

        string res;

        do
        {
            port.WriteLine("AT+GSN=1");
            await Task.Delay(250);
            res = port.ReadExisting();
        } while (!res.Contains("OK"));
    }
}