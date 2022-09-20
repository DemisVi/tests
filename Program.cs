using System;
using System.IO;

static class Prog
{
    static void Main()
    {
        var fileLister = new FileSearcher();
        int filesFound = 0;

        EventHandler<FileFoundArgs> onFileFound = (sender, eventArgs) =>
        {
            Console.WriteLine(eventArgs.FoundFile);
            filesFound++;
        };

        fileLister.FileFound += onFileFound;

        fileLister.Search(Directory.GetCurrentDirectory(), "*");
    }
}

public class FileFoundArgs : EventArgs
{
    public string FoundFile { get; }

    public FileFoundArgs(string fileName) => FoundFile = fileName;
}

public class FileSearcher
{
    public event EventHandler<FileFoundArgs> FileFound;

    public void Search(string directory, string searchPattern)
    {
        foreach (var file in Directory.EnumerateFiles(directory, searchPattern, new EnumerationOptions() { RecurseSubdirectories = true }))
        {
            RaiseFileFound(file);
        }
    }

    private void RaiseFileFound(string file) =>
        FileFound?.Invoke(this, new FileFoundArgs(file));
}