using System.Text;

System.Console.WriteLine("now" ==                                                       HumanTimeFormat.formatDuration(0));
System.Console.WriteLine("1 second" ==                                                  HumanTimeFormat.formatDuration(1));
System.Console.WriteLine("1 minute and 2 seconds" ==                                    HumanTimeFormat.formatDuration(62));
System.Console.WriteLine("2 minutes" ==                                                 HumanTimeFormat.formatDuration(120));
System.Console.WriteLine("1 hour, 1 minute and 2 seconds" ==                            HumanTimeFormat.formatDuration(3662));
System.Console.WriteLine("182 days, 1 hour, 44 minutes and 40 seconds" ==               HumanTimeFormat.formatDuration(15731080));
System.Console.WriteLine("4 years, 68 days, 3 hours and 4 minutes" ==                   HumanTimeFormat.formatDuration(132030240));
System.Console.WriteLine("6 years, 192 days, 13 hours, 3 minutes and 54 seconds" ==     HumanTimeFormat.formatDuration(205851834));
System.Console.WriteLine("8 years, 12 days, 13 hours, 41 minutes and 1 second" ==       HumanTimeFormat.formatDuration(253374061));
System.Console.WriteLine("7 years, 246 days, 15 hours, 32 minutes and 54 seconds" ==    HumanTimeFormat.formatDuration(242062374));
System.Console.WriteLine("3 years, 85 days, 1 hour, 9 minutes and 26 seconds" ==        HumanTimeFormat.formatDuration(101956166));
System.Console.WriteLine("1 year, 19 days, 18 hours, 19 minutes and 46 seconds" ==      HumanTimeFormat.formatDuration(33243586));

System.Console.WriteLine(uint.MaxValue);
System.Console.WriteLine(HumanTimeFormat.formatDuration(uint.MaxValue));

public class HumanTimeFormat
{
    public static string formatDuration(uint seconds)
    {
        if (seconds == 0) return "now";

        var spans = new[]
        {
            ("year", seconds / 31536000),
            ("day", (seconds / 86400) % 365),
            ("hour", (seconds / 3600) % 24),
            ("minute", (seconds / 60) % 60),
            ("second", seconds % 60),
        };

        var stringBuilder = new StringBuilder();
        var tempList = new List<string>(6);

        foreach (var i in spans)
        {
            Append(ref tempList, i);
        }

        static void Append(ref List<string> list, (string, uint) t)
        {
            var (i, j) = t;
            if (j == 0) return;

            list.Add(string.Format("{0} {1}", j, i + (j > 1 ? "s" : "")));
        }

        stringBuilder.Append(tempList[0]);
        for (var index = 1; index < tempList.Count - 1; index++)
        {
            stringBuilder.Append(", " + tempList[index]);
        }
        if (tempList.Count > 1) stringBuilder.AppendFormat(" and " + tempList.Last());

        return stringBuilder.ToString();
    }
}

// Years Days Hours Minutes Seconds

// seconds / 31536000, (seconds / 86400) % 365, (seconds / 3600) % 24, (seconds / 60) % 60, seconds % 60





