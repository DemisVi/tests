using System;
using System.Collections.Generic;
using System.Linq;

namespace ArteryISPProg.Models;

public class MemoryRow
{
    private byte[] _data = Array.Empty<byte>();
    public int Address { get; set; }
    public byte X00
    {
        get
        {
            return _data[0x00];
        }
        set
        {
            _data[0x00] = value;
        }
    }
    public byte X01
    {
        get
        {
            return _data[0x01];
        }
        set
        {
            _data[0x01] = value;
        }
    }
    public byte X02
    {
        get
        {
            return _data[0x02];
        }
        set
        {
            _data[0x02] = value;
        }
    }
    public byte X03
    {
        get
        {
            return _data[0x03];
        }
        set
        {
            _data[0x03] = value;
        }
    }
    public byte X04
    {
        get
        {
            return _data[0x04];
        }
        set
        {
            _data[0x04] = value;
        }
    }
    public byte X05
    {
        get
        {
            return _data[0x05];
        }
        set
        {
            _data[0x05] = value;
        }
    }
    public byte X06
    {
        get
        {
            return _data[0x06];
        }
        set
        {
            _data[0x06] = value;
        }
    }
    public byte X07
    {
        get
        {
            return _data[0x07];
        }
        set
        {
            _data[0x07] = value;
        }
    }
    public byte X08
    {
        get
        {
            return _data[0x08];
        }
        set
        {
            _data[0x08] = value;
        }
    }
    public byte X09
    {
        get
        {
            return _data[0x09];
        }
        set
        {
            _data[0x09] = value;
        }
    }
    public byte X0A
    {
        get
        {
            return _data[0x0A];
        }
        set
        {
            _data[0x0A] = value;
        }
    }
    public byte X0B
    {
        get
        {
            return _data[0x0B];
        }
        set
        {
            _data[0x0B] = value;
        }
    }
    public byte X0C
    {
        get
        {
            return _data[0x0C];
        }
        set
        {
            _data[0x0C] = value;
        }
    }
    public byte X0D
    {
        get
        {
            return _data[0x0D];
        }
        set
        {
            _data[0x0D] = value;
        }
    }
    public byte X0E
    {
        get
        {
            return _data[0x0E];
        }
        set
        {
            _data[0x0E] = value;
        }
    }
    public byte X0F
    {
        get
        {
            return _data[0x0F];
        }
        set
        {
            _data[0x0F] = value;
        }
    }

    public MemoryRow(byte[] data, int addressShift)
    {
        Address = Constants.StartAddress + addressShift;
        Set(data);
    }

    public MemoryRow Set(byte[] data)
    {
        var lastIndex = data.Length < Constants.RowLength ? data.Length : Constants.RowLength;
        _data = data[0..lastIndex];
        return this;
    }

    public static List<MemoryRow> Generate(byte[] data) =>
        data.Chunk(Constants.RowLength).Select((x, y) => new MemoryRow(x, y)).ToList();
}