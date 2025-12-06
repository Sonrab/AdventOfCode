namespace AoC.Day2;

public static class Comparisons
{
    public static bool HalvesAreEqual(this string input)
    {
        var mid = input.Length/2;
        return input[..mid] == input[mid..];
    }
        
    public static bool ThirdsAreEqual(this string input)
    {
        var t1 = input.Length/3;
        var t2 = t1*2;

        return input[..t1] == input[t1..t2] 
            && input[t1..t2] == input[t2..];
    }

    public static bool QuartersAreEqual(this string input)
    {
        var q1 = input.Length/4;
        var q2 = q1*2;
        var q3 = q1*3;

        return input[..q1] == input[q1..q2] 
            && input[q1..q2] == input[q2..q3]
            && input[q2..q3] == input[q3..];
    }

    public static bool FifthsAreEqual(this string input)
    {
        var f1 = input.Length/5;
        var f2 = f1*2;
        var f3 = f1*3;
        var f4 = f1*4;

        return input[..f1] == input[f1..f2] 
            && input[f1..f2] == input[f2..f3]
            && input[f2..f3] == input[f3..f4]
            && input[f3..f4] == input[f4..];
    }
}