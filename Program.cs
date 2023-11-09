var host = Host.CreateDefaultBuilder()
               .ConfigureServices(x => x.AddSingleton<ICU, CU>()
                                        .AddSingleton<IWriter, Writer>())
               .Build();

host.Services.GetService<IWriter>()?.Run();

public interface IWriter
{
    public void Run();
}

public class Writer : IWriter
{
    private ICU _cu;
    private IHost _host;
    public Writer(IHost host, ICU contactUnit)
    {
        _host = host;
        _cu = contactUnit;
    }

    public void Run()
    { 
        System.Console.WriteLine("Writer ran"); 
        _cu.Run();
        _host.Services.GetRequiredService<ICU>();
    }
}

public interface ICU { public void Run(); }

public class CU : ICU
{
    public void Run()
    { System.Console.WriteLine("CU ran"); }
}
