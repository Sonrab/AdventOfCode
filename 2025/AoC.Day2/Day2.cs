
using AoC.Day2;
using AoC.Shared;

double Part1()
{
    var ranges = "AoC.Day2/input.txt".Read().Split(',').Select(r =>
    {
        var split = r.Split('-');
        return (double.Parse(split[0]), double.Parse(split[1]));
    });

    double invalids = 0;
    foreach(var (start, end) in ranges)
    {
        for(double i = start; i <= end; i++)
        {
            var s = i.ToString();
            if(s.HalvesAreEqual())
            {
                invalids += i;
            }
        }
    }

    return invalids;
}

double Part2()
{
    var ranges = "AoC.Day2/input.txt".Read().Split(',').Select(r =>
    {
        var split = r.Split('-');
        return (double.Parse(split[0]), double.Parse(split[1]));
    });

    double invalids = 0;
    foreach(var (start, end) in ranges)
    {
        for(double i = start; i <= end; i++)
        {
            var s = i.ToString();
            var distinct = s.Distinct().Count();
            
            if(i < 10) continue;
            else if(distinct == 1) invalids += i;
            else if(s.Length is 2 or 3 or 5 or 7) continue;
            else if(s.Length % 2 == 0 && s.HalvesAreEqual()) invalids += i;
            else if(s.Length % 3 == 0 && s.ThirdsAreEqual()) invalids += i;
            else if(s.Length % 4 == 0 && s.QuartersAreEqual()) invalids += i;
            else if(s.Length % 5 == 0 && s.FifthsAreEqual()) invalids += i;
        }
    }

    return invalids;
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());
;