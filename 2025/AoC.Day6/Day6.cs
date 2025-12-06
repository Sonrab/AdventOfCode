using System.Data;
using AoC.Shared;

static long Part1()
{
    var rows = "AoC.Day6/input.txt".Read().SplitLines()
        .Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToArray())
        .ToArray();

    return rows[^1].Select((m, i) =>
    {
        var nums = rows[..^1].Select(row => long.Parse(row[i]));
        return m switch
        {
            "+" => nums.Sum(),
            "*" => nums.Aggregate<long, long>(1, (p, n) => n * p),
            _ => throw new Exception()
        };
    }).Sum();
}

static long Part2()
{
    var rows = "AoC.Day6/input.txt".Read().SplitLines().ToArray();

    List<string[]> problems = [];
    var tracker = 0;
    var symbolRow = rows[^1];

    for(int i = 1; i < symbolRow.Length-1; i++)
    {
        if(symbolRow[i] == ' ') continue;

        problems.Add([ ..rows.Select(row => row[tracker..(i-1)]) ]);
        tracker = i;
    }
    problems.Add([ ..rows.Select(row => row[tracker..]) ]); // postprocess på sista

    return problems.Select(values =>
    {
        var symbol = values[^1][0];
        var max = values.MaxBy(s => s.Length)!.Length;
        var nums = Enumerable.Range(0, max).Select(i =>
            long.Parse(values[..^1].Aggregate(string.Empty, (s, pv) => pv[i] == ' ' ? s : s + pv[i])))
            .ToArray();

        return symbol switch
        {
            '+' => nums.Sum(),
            '*' => nums.Aggregate<long, long>(1, (p, n) => n * p),
            _ => throw new Exception()
        };
    }).Sum();
}
Console.WriteLine(Part1());
Console.WriteLine(Part2());
;