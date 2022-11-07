using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class ModemComparer : IEqualityComparer<Modem>
{
    public bool Equals(Modem? x, Modem? y)
    {
        if (Object.ReferenceEquals(x, y)) return true;

        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            return false;

        return x.Serial == y.Serial;
    }

    public int GetHashCode([DisallowNull] Modem obj)
    {
        if (Object.ReferenceEquals(obj, null)) return 0;

        int hashProductName = obj.AttachedTo == null ? 0 : obj.AttachedTo.GetHashCode();

        int hashProductCode = obj.Serial.GetHashCode();

        return hashProductName ^ hashProductCode;

    }
}
