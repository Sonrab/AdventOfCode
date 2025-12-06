namespace AoC.Shared;

public static class Extensions
{
    public static string Read(this string path)
    {
        var env_home = Environment.GetEnvironmentVariable("HOME");
        return File.ReadAllText($"{env_home}/Repositories/AdventOfCode/2025/{path}");
    }
    public static string[] SplitLines(this string input)
    {
        return GeneratedRegex.MatchLineBreak().Split(input);
    }
}
