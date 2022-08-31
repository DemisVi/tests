using System;
using System.Threading.Tasks;

static class Program
{
    static async Task Fail()
    {
        await Task.Delay(1);
        throw new InvalidOperationException();
    }

    static void Main()
    {
        try
        {
            Fail().Wait();
            Console.Out.Write("_failed");
        }
        catch (InvalidOperationException)
        {
            Console.Out.Write("_caught");
        }
        catch (AggregateException ex)
        {
            System.Console.WriteLine(ex.InnerException);
        }
    }
}