using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Days;

public class Day02 : AbstractDay
{
    [Benchmark]
    public override void Execute()
    {
        var lines = GetInput();
        var totalSafe = 0;
        foreach (var line in lines)
        {
            var levels = line.Split(" ");
            if (IsSafeSequence(levels) || CanBeMadeSafe(levels))
            {
                totalSafe++;
            }
        }
        Result(totalSafe);
    }

    private bool IsSafeSequence(string[] levels)
    {
        var orderedAscending = levels.OrderBy(x => int.Parse(x)).ToArray();
        var orderedDescending = levels.OrderByDescending(x => int.Parse(x)).ToArray();

        if (!levels.SequenceEqual(orderedAscending) && !levels.SequenceEqual(orderedDescending))
            return false;

        for (int i = 0; i < levels.Length - 1; i++)
        {
            if (levels[i] == levels[i + 1])
                return false;

            if (Math.Abs(int.Parse(levels[i]) - int.Parse(levels[i + 1])) > 3)
                return false;
        }

        return true;
    }

    private bool CanBeMadeSafe(string[] levels)
    {
        // Try removing one number at a time and check if the resulting sequence is safe
        for (int i = 0; i < levels.Length; i++)
        {
            var subList = levels.Take(i).Concat(levels.Skip(i + 1)).ToArray();
            if (IsSafeSequence(subList))
                return true;
        }
        return false;
    }
}