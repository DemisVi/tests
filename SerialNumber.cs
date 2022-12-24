using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wrench.Model;
// процедуры работы с серийным номером изделия
public class SerialNumber
{
    // private members
    private const string val34 = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
    private int len34 = 4; // число символов 34-ричного номера
    private string lastSnFile;
    private string sn34 = string.Empty;
    private int lastSN;
    private int baseSN;

    //=======================================

    public SerialNumber(string snFileName, int snSize = 4)
    {
        baseSN = 0;
        lastSnFile = snFileName;
        len34 = snSize;
        LoadSerial();
    } // SerialNumber()
      //----------------------------------------


    public void LoadSerial()
    {
        if (!File.Exists(lastSnFile)) return;
        using (FileStream fs = new FileStream(lastSnFile, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader br = new BinaryReader(fs))
            { lastSN = br.ReadInt32(); }
        }
        if (lastSN < baseSN)
            lastSN = baseSN;
    } // void LoadSerial()
      //------------------------------------


    private void SaveSerial()
    {
        using (BinaryWriter bw = new BinaryWriter(File.Open(lastSnFile, FileMode.Create)))
        {
            bw.Write(lastSN);
        }
    } // SaveSerial()
      //------------------------------------------------

    public void SetSNbase(int snBase)
    {
        baseSN = snBase;
        if (lastSN < baseSN)
            lastSN = baseSN;
    } //  void SetSNbase
      //----------------------------


    public void ResetSN()
    {
        lastSN = baseSN;
        SaveSerial();
    } // void ResetSN()
      //--------------------------------


    // назначает текущий номер и сохраняет его как последний (не базовый)
    public void SetCurrentSN(int Value)
    {
        lastSN = Value;
        SaveSerial();
    } // void SetCurrentSN()
      //-------------------------------------------


    public int GetLastSN()
    {
        return lastSN;
    }
    //------------------------------------------


    public int GetNextSN()
    {
        lastSN++;
        SaveSerial();
        sn34 = intTo34(lastSN);
        return lastSN;
    } // int GetNextSN()
      //------------------------------------


    // пропускает SN с недопустимыми символами ('O','I')
    public string GetNextSN34()
    {
        lastSN++;
        SaveSerial();
        sn34 = intTo34(lastSN);

        while (sn34.Length < len34)
        {
            sn34.Insert(0, "0");
        }
        return sn34;
    } // string GetNextSN34()
      //----------------------------------


    // комбинация sn34&(int)
    public string GetLongSerial(int decNum)
    {
        return String.Format($"{intTo34(decNum)} ({decNum:D7})");
    } // string GetLongSerial()
      //-----------------------------------------------


    public string intTo34(int value)
    {
        int rest;
        string result = "";
        do
        {
            rest = value % 34;
            result = val34[rest].ToString() + result;
            value /= 34;
        } while (value != 0);

        int len = len34 - result.Length;
        while (len > 0)
        {
            result = "0" + result;
            len--;
        }
        return result;
    } // IntTo34
      //-----------------------------------------

    // декодирует строку 34-ричного формата
    public int thirtyFourToInt(string sn34)
    {
        sn34 = sn34.ToUpper();
        int intValue = 0;
        int order = 1;
        int numLen = sn34.Length;

        for (int i = 0; i < numLen; i++)
        {
            int ndx = val34.IndexOf(sn34[numLen - 1 - i]);
            if (ndx >= 0)
            {
                intValue = ndx * order + intValue;
                order *= 34;
            }
            else throw new Exception("Неправильный символ в 34-ричном значении.");
        }
        return intValue;
    } // thirtyFourToInt
      //-------------------------------------------------------


    public string GetDeviceSerial()
    {
        return $"T7G84178{DateTime.Now.DayOfYear:D3}{(DateTime.Now.Year % 100):D2}0000{sn34}";
    }
}
