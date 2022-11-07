using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.IO.Ports;
using ROOT.CIMV2.Win32;

public class TelitLocator
{
    private List<SerialPort> _serialsList = new List<SerialPort>();
    private List<string[]> _modemInfo = new List<string[]>();
    private List<Modem> _res = new List<Modem>();

    public async Task<IEnumerable<Modem>> LocateDevicesAsync()
    {
        foreach (POTSModem item in POTSModem.GetInstances("ProviderName = 'Telit'"))
            _serialsList.Add(new SerialPort($"{item.AttachedTo}")
            {
                Handshake = Handshake.RequestToSend,
                NewLine = "\r",
                WriteTimeout = 200,
                ReadTimeout = 200,
                BaudRate = 115200,
            });

        foreach (var item in _serialsList)
        {
            var tempStringBuilder = new StringBuilder();
            tempStringBuilder.AppendLine(item.PortName);

            if (!item.IsOpen) item.Open();
            await item.ModemStartAsync();

            item.WriteLine("AT+GSN=0");

            await Task.Delay(250);

            tempStringBuilder.Append(item.ReadExisting());

            item.Close();

            _modemInfo.Add(tempStringBuilder.ToString().Split(new[] { '\r', '\n' },
                                                              StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));
        }

        foreach (var item in _modemInfo)
            foreach (var inneritem in item)
                if (long.TryParse(inneritem, out var sn))
                    _res.Add(new Modem() { AttachedTo = item.First(), Serial = sn.ToString() });

        return _res.DistinctBy(x => x.Serial);
    }
}