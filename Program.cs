using System.Text.Json;

var str = ConfigHelper.Token;
System.Console.WriteLine(str);
ConfigHelper.Users?.Add("qweqweqwe", "123123123123");
ConfigHelper.Save();

public static class ConfigHelper
{
    private static Config config;
    public static string Token => config.Token;
    public static Dictionary<string, string>? Users => config.Users;
    static ConfigHelper()
    {
        if (File.Exists("config.json"))
        {
            System.Console.WriteLine("read config.json");
            using var fileStream = File.OpenRead("config.json");
            config = JsonSerializer.Deserialize<Config>(fileStream);
        }
        else
        {
            System.Console.WriteLine("create config.json");
            config = new Config();
            ConfigHelper.Save(config);
        }
    }

    private static void Save(Config config)
    {
        System.Console.WriteLine("save config.json");
        using var fileStream = File.OpenWrite("config.json");
        JsonSerializer.Serialize(fileStream, config);
    }
    public static void Save() => Save(config);
}

public struct Config
{
    public string Token { get; set; } = "string";
    public Dictionary<string, string>? Users { get; set; } = new();

    public Config() { }
}