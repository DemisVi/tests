using var source = File.OpenRead("./Program.cs");
using var destination = File.Create("./out");

await TransferSymbols(source, destination);

static async Task TransferSymbols(Stream source, Stream destination)
{   
    var buffer = new byte[source.Length];

    var charCount = 0;

    await source.ReadAsync(buffer, 0, buffer.Length);

    foreach (char i in buffer)
    {
        if (char.IsLetter(i) && char.IsLower(i))
        {
            buffer[charCount] = Convert.ToByte(i);
            charCount++;
        }
    }
    await destination.WriteAsync(buffer, 0, charCount);
}