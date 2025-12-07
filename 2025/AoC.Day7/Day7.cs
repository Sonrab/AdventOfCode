using System.Data;
using AoC.Shared;

static long Part1()
{
    var rows = "AoC.Day7/input.txt".ReadAllLines();

    return rows.Skip(1).Aggregate(
        (Splits: 0, Beams: new Position[] { new(rows[0].IndexOf('S'), 1) }), 
        (input, row) =>
    {
        var splitters = row.Select((c, i) => (c, i)).Where(p => p.c == '^').Select(p => p.i);
        var splits = input.Splits + input.Beams.Where(b => splitters.Contains(b.X)).Count();

        Position[] beams = [.. input.Beams.SelectMany<Position, Position>(b =>
        {
            return splitters.Contains(b.X)
                ? [ b with { X = b.X-1, Y = b.Y+1}, b with { X = b.X+1, Y = b.Y+1} ]
                : [ b with { Y = b.Y+1} ];
        }).Distinct() ];

        return (splits, beams);
    }).Splits;
}

static long Part2()
{
    var rows = "AoC.Day7/input.txt".ReadAllLines();

    return rows.Skip(1).Aggregate(
        new PositionMedAntal[] { new(Position: new(rows[0].IndexOf('S'), 1), Antal: 1)}, 
        (beamsIn, row) =>
    {
        var splitters = row.Select((c, i) => (c, i)).Where(p => p.c == '^').Select(p => p.i);

        PositionMedAntal[] splits = [.. beamsIn.SelectMany<PositionMedAntal, PositionMedAntal>(b =>
        {
            return splitters.Contains(b.Position.X)
                ? [ b with { Position = b.Position with { X = b.Position.X-1, Y = b.Position.Y+1 } }, 
                    b with { Position = b.Position with { X = b.Position.X+1, Y = b.Position.Y+1 } } ]
                : [ b with { Position = b.Position with { Y = b.Position.Y+1 } }];
        })];

        var beamsOut = splits.GroupBy(b => b.Position).Select(g => g.First() with { Antal = g.Sum(a => a.Antal) } );
        
        return [.. beamsOut];
    }).Sum(b => b.Antal);
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());
;

record Position(int X, int Y);
record PositionMedAntal(Position Position, long Antal);