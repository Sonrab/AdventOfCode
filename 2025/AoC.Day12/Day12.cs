using AoC.Shared;

static long Part1()
{
    return "AoC.Day12/input.txt".ReadAllLines()
        .Skip(30)
        .Count(row =>
        {
            var split = row.Split(':');
            var allowedArea = split[0].Split('x', StringSplitOptions.RemoveEmptyEntries)
                .Select(v => long.Parse(v)).Multiply();
            var lazyOccupiedArea = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Sum(v => long.Parse(v)) * 9;

            return allowedArea >= lazyOccupiedArea;
        });
}

Console.WriteLine(Part1());