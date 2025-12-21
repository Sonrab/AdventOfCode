using System.Data;
using AoC.Shared;

static long Part1()
{
    var positions = "AoC.Day9/input.txt".ReadAllLines()
        .Select(row =>
        {
            var split = row.Split(',');
            return new Point(int.Parse(split[0]), int.Parse(split[1]));
        });

    return positions.Aggregate<Point, long>(0, (largest, outerPos) =>
    {
        var max = positions.Max(innerPos => outerPos.RectangleSize(innerPos));
        return max > largest ? max : largest;
    });
}

static long Part2()
{
    var positions = "AoC.Day9/input.txt".ReadAllLines()
        .Select(row =>
        {
            var split = row.Split(',');
            return new Point(int.Parse(split[0]), int.Parse(split[1]));
        });

    var validated = new HashSet<Rectangle>();

    var kanter = positions.Zip(positions.Skip(1)).Select(p => new Kant(p.First, p.Second)).ToArray();
    var polygon = new Polygon(kanter);

    return positions.Aggregate<Point, long>(0, (largest, outerPos) =>
    {
        var max = positions.Max(innerPos =>
        {
            if(innerPos == outerPos) return 0;

            var rect = Rectangle.From2Corners(outerPos, innerPos);
            if(validated.Contains(rect))
            {
                return 0;
            }

            validated.Add(rect);
            return rect.Corners.All(c => polygon.PunktInomPolygon(c)) && polygon.ValideraRektangel(rect)
                ? outerPos.RectangleSize(innerPos)
                : 0;
        });
        return max > largest ? max : largest;
    });
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());
;

record Point(long X, long Y)
{
    public long RectangleSize(Point other) => (Math.Abs(X-other.X)+1) * (Math.Abs(Y-other.Y)+1);
}

record Kant(Point P1, Point P2)
{
    public long X1 = Math.Min(P1.X, P2.X);
    public long X2 = Math.Max(P1.X, P2.X);    
    public long Y1 = Math.Min(P1.Y, P2.Y);
    public long Y2 = Math.Max(P1.Y, P2.Y);
    public bool IsVertikal = P1.X == P2.X;
    public bool IsHorisontell = P1.Y == P2.Y;

    public bool IsPointOnKant(Point point)
    {
        if ((IsVertikal && P1.X == point.X && Y1 <= point.Y && Y2 >= point.Y) || 
            (IsHorisontell && P1.Y == point.Y && X1 <= point.X && X2 >= point.X))
        {
            return true;
        }

        return false;
    } 

    public bool KorsarLinjeVertikalKant(Point point)
    {
        return IsVertikal && Y1 <= point.Y && Y2 > point.Y && X1 > point.X;
    }
}

record Polygon(Kant[] Kanter)
{
    public bool PunktInomPolygon(Point p)
    {
        var innanför = false;
        foreach(var kant in Kanter)
        {
            if(kant.IsPointOnKant(p))
            {
                return true;
            }

            if(kant.KorsarLinjeVertikalKant(p))
            {
                innanför = !innanför;
            }
        }

        return innanför;
    }

    public bool ValideraRektangel(Rectangle rect)
    {
        foreach (var sida in rect.Sides)
        {
            foreach (var kant in Kanter)
            {
                if (sida.IsHorisontell == kant.IsHorisontell) continue;

                if (sida.IsVertikal && kant.IsHorisontell)
                {
                    // Kolla att skärningspunkten befinner sig inom spannet för både kanten samt rektangelsidan 
                    if((kant.Y1 > sida.Y1) && (kant.Y1 < sida.Y2) &&
                        (sida.X1 > kant.X1) && (sida.X1 < kant.X2))
                    {
                        return false;
                    }
                }
                else if (sida.IsHorisontell && kant.IsVertikal)
                {
                    // Kolla att skärningspunkten befinner sig inom spannet för både kanten samt rektangelsidan 
                    if(kant.X1 > sida.X1 && kant.X1 < sida.X2 &&
                        sida.Y1 > kant.Y1 && sida.Y1 < kant.Y2)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}

record Rectangle(Point TopLeft, Point TopRight, Point BottomLeft, Point BottomRight)
{
    public Point[] Corners => [TopLeft, TopRight, BottomLeft, BottomRight];

    public Kant[] Sides => [
        new(TopLeft, TopRight), // Över
        new(TopLeft, BottomLeft), // Vänster
        new(BottomLeft, BottomRight), // Under
        new(TopRight, BottomRight)]; // Höger

    public static Rectangle From2Corners(Point p1, Point p2) => new(
        TopLeft: new Point(Math.Min(p1.X, p2.X), Math.Max(p1.Y, p2.Y)),
        TopRight: new Point(Math.Max(p1.X, p2.X), Math.Max(p1.Y, p2.Y)),
        BottomLeft: new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y)),
        BottomRight: new Point(Math.Max(p1.X, p2.X), Math.Min(p1.Y, p2.Y)));   
}