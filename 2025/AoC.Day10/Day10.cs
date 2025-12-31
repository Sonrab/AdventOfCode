using System.Text.RegularExpressions;
using AoC.Day10;
using AoC.Shared;

static long Part1()
{
    var rows = "AoC.Day10/input.txt".ReadAllLines();

    return rows.Sum(row =>
    {
        var indicatorLightDiagram = Regex.Match(row, @"\[(.*)\]").Groups[1].Value
            .Select(v => v == '.' ? 0 : 1).ToArray();
        var wiringSchematics = Regex.Matches(row, @"(?<=\()[^)]*(?=\))")
            .Select(m => m.Value.Split(",").Select(s => int.Parse(s))).ToArray();

        for(int i = 1; i <= wiringSchematics.Length; i++)
        {
            int[] arr = [.. Enumerable.Range(0, wiringSchematics.Length)];
            var schematicIndexCombinations = Helper.FindCombinations(arr, i);

            foreach (var schematicIndexes in schematicIndexCombinations) 
            {
                var indicatorLights = new int[indicatorLightDiagram.Length];
                var flips = schematicIndexes.SelectMany(s => wiringSchematics[s]);

                foreach (var flip in flips) 
                {
                    indicatorLights[flip] = indicatorLights[flip] == 0 ? 1 : 0;
                }

                if(indicatorLightDiagram.SequenceEqual(indicatorLights))
                {
                    return i;
                }
            }
        }

        throw new Exception();
    });
}

static long Part2()
{
    var rows = "AoC.Day10/input.txt".ReadAllLines();

    return 0;
}

Console.WriteLine(Part1());
Console.WriteLine(Part2());
;