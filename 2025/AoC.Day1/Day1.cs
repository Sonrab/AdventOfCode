
using AoC.Shared;

static int Part1()
{
    var rows = "AoC.Day1/input.txt".Read().SplitLines();
    var (_, points) = rows.Aggregate((Dial: 50, DialPoints: 0),  (input, r) =>
    {
        var dialRaw = r[0] == 'L' ? input.Dial - int.Parse(r[1..]) : input.Dial + int.Parse(r[1..]);

        var dial = dialRaw % 100 is var modres && modres < 0 
            ? modres + 100 
            : modres;

        var dialpoints = dial == 0 
            ? input.DialPoints + 1 
            : input.DialPoints;

        return (dial, dialpoints);
    });

    return points;
}

static int Part2()
{
    var rows = "AoC.Day1/input.txt".Read().SplitLines();
    var (_, points) = rows.Aggregate((Dial: 50, DialPoints: 0),  (input, r) =>
    {        
        var dialRaw = r[0] == 'L' ? input.Dial - int.Parse(r[1..]) : input.Dial + int.Parse(r[1..]);
        var points = dialRaw is < 0 && input.Dial != 0
            ? Math.Abs(dialRaw/100) + 1
            : Math.Abs(dialRaw/100);

        var dial = dialRaw % 100 is var modres && modres < 0
            ? modres + 100
            : modres;

        var dialPoints = dial == 0
            ? input.DialPoints + points + 1 
            : input.DialPoints + points;

        if(points > 0 && dial == 0)
        {
            dialPoints--;
        }

        return (dial, dialPoints);
    });

    return points;
}

var p1 = Part1();
var p2 = Part2();
; 


