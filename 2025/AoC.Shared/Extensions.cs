
using Microsoft.VisualBasic;

namespace AoC.Shared;

public static class Extensions
{
    public static string Read(this string path)
    {
        var env_home = Environment.GetEnvironmentVariable("HOME");
        return File.ReadAllText($"{env_home}/Repositories/AdventOfCode/2025/{path}");
    }

    public static string[] ReadAllLines(this string path)
    {
        var env_home = Environment.GetEnvironmentVariable("HOME");
        return File.ReadAllLines($"{env_home}/Repositories/AdventOfCode/2025/{path}");
    }

    public static int Multiply(this IEnumerable<int> nums) 
    {
        return nums.Aggregate(1, (p, n) => n * p);
    }

    public static long Multiply(this IEnumerable<long> nums) 
    {
        return nums.Aggregate<long, long>(1, (p, n) => n * p);
    }
}
