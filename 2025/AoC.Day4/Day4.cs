using System.Data;
using AoC.Shared;

static long Part1()
{
    var positions = "AoC.Day4/input.txt".ReadAllLines()
        .SelectMany((row, i) => row.Select((col, j) => (X: j, Y: i, Symbol: col)))
        .ToDictionary(key => new Position(key.X, key.Y), val => val.Symbol);

    return positions.Where(p => p.Value == '@').Select(p => p.Key).Count(pos => 
        pos.GetAdjacent().Count(a => positions.TryGetValue(a, out var c) && c == '@') < 4);
}

static long Part2()
{
    var positions = "AoC.Day4/input.txt".ReadAllLines()
        .SelectMany((row, i) => row.Select((col, j) => (X: j, Y: i, Symbol: col)))
        .ToDictionary(key => new Position(key.X, key.Y), val => val.Symbol);

    var maxX = positions.Keys.MaxBy(p => p.X)!.X;
    var maxY = positions.Keys.MaxBy(p => p.Y)!.Y;

    var processingPile = new HashSet<Position>(positions.Where(p => p.Value == '@').Select(p => p.Key));
    var rollsRemoved = 0;

    while(processingPile.Count > 0)
    {
        var pos = processingPile.First();
        processingPile.Remove(pos);

        if(pos.GetAdjacent().Count(a => positions.TryGetValue(a, out var c) && c == '@') < 4)
        {
            rollsRemoved++;
            positions[pos] = '.';

            processingPile.UnionWith(pos.GetAdjacent().Where(p => 
                p.X >= 0 && p.X <= maxX &&
                p.Y >= 0 && p.Y <= maxY &&
                positions[p] == '@'));
        }
    }

    return rollsRemoved;
}
Console.WriteLine(Part1());
Console.WriteLine(Part2());
;

record Position(int X, int Y)
{
    public IEnumerable<Position> GetAdjacent() => [
        new(X-1, Y-1), // Top left
        new(X, Y-1), // Top
        new(X+1, Y-1), // Top right
        new(X-1, Y), // Left
        new(X+1, Y), // Right
        new(X-1, Y+1), // Bottom left
        new(X, Y+1), // Bottom
        new(X+1, Y+1) // Bottom right
    ];
};