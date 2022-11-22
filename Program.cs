using System;
using System.Linq;
using System.Reflection;
using System.Management;

using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PNPEntity WHERE Manufacturer LIKE 'Samsung%'");

var query = searcher.Get();

var @enum = query.GetEnumerator();
@enum.MoveNext();

foreach (var i in @enum.Current.Properties)
{
    Type t = i.GetType();
    Console.WriteLine("Type is: {0}", t.Name);
    var props = t.GetProperties();
    Console.WriteLine("Properties (N = {0}):",
                      props.Length);
    foreach (var prop in props)
        if (prop.GetIndexParameters().Length == 0)
            Console.WriteLine("   {0} ({1}): {2}", prop.Name,
                              prop.PropertyType.Name,
                              prop.GetValue(i));
        else
            Console.WriteLine("   {0} ({1}): <Indexed>", prop.Name,
                              prop.PropertyType.Name);
}