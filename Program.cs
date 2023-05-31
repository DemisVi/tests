using System;

var v = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.Machine);

Environment.SetEnvironmentVariable("path", "C:\\;" + v, EnvironmentVariableTarget.Machine);

System.Console.WriteLine(Environment.GetEnvironmentVariable("path"));
