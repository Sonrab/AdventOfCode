using System.Data;
using AoC.Shared;

static long Part1()
{
    var rows = "AoC.Day5/input.txt".ReadAllLines();
    var ranges = rows.TakeWhile(r => r.Length > 0)
        .Select(row => 
        {
            var split = row.Split('-');
            return new IdRange(long.Parse(split[0]), long.Parse(split[1]));
        }).ToArray();
    var ids = rows.Reverse().TakeWhile(r => r.Length > 0).Select(long.Parse);

    return ids.Count(id => ranges.Any(r => r.IsInRange(id)));
}

static long Part2()
{
    var rows = "AoC.Day5/input.txt".ReadAllLines();
    var ranges = rows.TakeWhile(r => r.Length > 0)
        .Select(row => 
        {
            var split = row.Split('-');
            return new IdRange(long.Parse(split[0]), long.Parse(split[1]));
        }).ToHashSet();

    var finishedRanges = new HashSet<IdRange>();

    while(ranges.FirstOrDefault() is { } range)
    {
        var overlapping = ranges.Where(range.IsOverlapping).Append(range).ToArray();
        ranges.RemoveWhere(r => overlapping.Contains(r));

        if(overlapping.Length == 1)
        {
            finishedRanges.Add(overlapping[0]);
        }
        else
        {
            ranges.Add(new(
                Start: overlapping.MinBy(r => r.Start)!.Start,
                End: overlapping.MaxBy(r => r.End)!.End));
        }
    }

    return finishedRanges.Sum(r => r.NumberOfIds());
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());


record IdRange(long Start, long End)
{
    public bool IsInRange(long value) => value >= Start && value <= End;

    public bool IsOverlapping(IdRange range) => this != range && 
        (range.Start >= Start && range.Start <= End || 
            range.End >= Start && range.End <= End ||
            range.Start < Start && range.End > End);

    public long NumberOfIds() => End-Start+1;
}