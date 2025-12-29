using System.Data;
using AoC.Shared;

static long Part1()
{
    var points = "AoC.Day8/input.txt".ReadAllLines().Select(row =>
        {
            var split = row.Split(',').Select(r => int.Parse(r)).ToArray();
            return new Point(split[0], split[1], split[2]);
        });

    var closestConnections = points
        .SelectMany(p1 => points.Select(p2 => new Connection(p1, p2, p1.GetDistanceBetweenPoints(p2))))
        .DistinctBy(c => c.DistinctIdentifier).OrderBy(c => c.Distance).Take(1000);

    List<HashSet<Point>> networks = [];

    foreach(var connection in closestConnections)
    {
        var matchingNetworks = networks.Where(n => n.Contains(connection.P1) || n.Contains(connection.P2)).ToArray() ?? [];

        if(matchingNetworks.Length == 2) // Kombinera två nätverk
        {
            matchingNetworks[0].UnionWith(matchingNetworks[1]);
            matchingNetworks[0].Add(connection.P1);
            matchingNetworks[0].Add(connection.P2);

            networks.Remove(matchingNetworks[1]);
        }
        else if(matchingNetworks.Length == 1) // Lägg till i existerande nätverk
        {
            matchingNetworks[0].Add(connection.P1);
            matchingNetworks[0].Add(connection.P2);
        }
        else
        {
            networks.Add([connection.P1, connection.P2]); // Starta nytt nätverk
        }
    }

    return networks.Select(n => n.Count).OrderDescending().Take(3).Multiply();
}

static long Part2()
{
    var points = "AoC.Day8/input.txt".ReadAllLines().Select(row =>
        {
            var split = row.Split(',').Select(r => int.Parse(r)).ToArray();
            return new Point(split[0], split[1], split[2]);
        }).ToArray();

    var connections = points
        .SelectMany(p1 => points.Select(p2 => new Connection(p1, p2, p1.GetDistanceBetweenPoints(p2))))
        .DistinctBy(c => c.DistinctIdentifier).OrderBy(c => c.Distance).ToArray();

    List<HashSet<Point>> networks = [];

    foreach(var connection in connections)
    {
        var matchingNetworks = networks.Where(n => n.Contains(connection.P1) || n.Contains(connection.P2)).ToArray() ?? [];
        
        if(matchingNetworks.Length == 2) // Kombinera två nätverk
        {
            matchingNetworks[0].UnionWith(matchingNetworks[1]);
            matchingNetworks[0].Add(connection.P1);
            matchingNetworks[0].Add(connection.P2);

            networks.Remove(matchingNetworks[1]);
        }
        else if(matchingNetworks.Length == 1) // Lägg till i existerande nätverk
        {
            matchingNetworks[0].Add(connection.P1);
            matchingNetworks[0].Add(connection.P2);
        }
        else
        {
            networks.Add([connection.P1, connection.P2]); // Starta nytt nätverk
        }

        if(networks.First().Count == points.Length)
        {
            return connection.P1.X * connection.P2.X;
        }
    }

    throw new Exception();
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());
;

record Point(long X, long Y, long Z)
{
    public long OrderValue = long.Parse($"{X}{Y}{Z}");

    public double GetDistanceBetweenPoints(Point point)
    {
        if(this == point) return double.MaxValue;

        long dx = X-point.X;
        long dy = Y-point.Y;
        long dz = Z-point.Z;

        return dx*dx + dy*dy + dz*dz;
    }
}

record Connection(Point P1, Point P2, double Distance)
{
    public string DistinctIdentifier = $"{Math.Min(P1.OrderValue, P2.OrderValue)}|{Math.Max(P1.OrderValue, P2.OrderValue)}";
}