using AoC.Shared;

static int Part1()
{
    var rows = "AoC.Day3/input.txt".ReadAllLines();
    return rows.Sum(row =>
    {
        var valuesWithIndex = row.Select((v, i) => (Value: (int)char.GetNumericValue(v), Index: i)).ToArray();
        var first = valuesWithIndex[..^1].OrderByDescending(n => n.Value).ThenBy(n => n.Index).First();
        var second = valuesWithIndex[(first.Index+1)..].OrderByDescending(n => n.Value).ThenBy(n => n.Index).First();

        return int.Parse($"{first.Value}{second.Value}");
    });
}

static long Part2()
{
    var rows = "AoC.Day3/input.txt".ReadAllLines();

    return rows.Sum(row =>
    {
        var valuesWithIndex = row.Select((v, i) => (Value: (int)char.GetNumericValue(v), Index: i)).ToArray();
        var joltage = Enumerable.Range(0, 12).Reverse().Aggregate((Start: 0, Joltage: string.Empty), (input, skipLast) =>
        {
            var battery = valuesWithIndex[input.Start..^skipLast].OrderByDescending(n => n.Value).ThenBy(n => n.Index).First();
            return (battery.Index+1, input.Joltage += battery.Value);
        }).Joltage;

        return long.Parse(joltage);
    });
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());
;