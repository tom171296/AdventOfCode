using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Days;

public class Day01 : AbstractDay
{
    [Benchmark]
    public override void Execute()
    {
        var leftColumn = new List<int>();
        var rightColumn = new List<int>(); 
        var lines = GetInput();

        // 3   4

        foreach (var line in lines)
        {
            var parts = line.Split("   ");
            leftColumn.Add(int.Parse(parts[0]));
            rightColumn.Add(int.Parse(parts[1]));
        }

        var totalDistance = 0;

        while (leftColumn.Count > 0)
        {
            var leftLowest = leftColumn.Min();
            var rightLowest = rightColumn.Min();

            // distance between left and right
            var distance = Math.Abs(leftLowest - rightLowest);

            totalDistance += distance;

            // remove the lowest values
            leftColumn.Remove(leftLowest);
            rightColumn.Remove(rightLowest);
        }



        Result(totalDistance);
        
    }
}