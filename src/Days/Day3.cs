using System.Text.RegularExpressions;
using Microsoft.Diagnostics.Tracing.Parsers.Tpl;

namespace AdventOfCode2024.Days;

public class Day03 : AbstractDay
{
    public override void Execute()
    {
        var input = File.ReadAllText("input.txt");
        var foundMultiplier = new List<(int, int)>();

    
        // Match mul() patterns only if the closest preceding control is do()
        var matches = Regex.Matches(input, @"(?:(?:(?:(?<!don't\()[^d]*?)|^)do\(\)[^d]*?)?mul\((\d{1,3}),\s*(\d{1,3})\)");
        Console.WriteLine("count " + matches.Count);
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Groups[1].Value);
            Console.WriteLine(match.Groups[2].Value);

            var a = int.Parse(match.Groups[1].Value);
            var b = int.Parse(match.Groups[2].Value);
            foundMultiplier.Add((a, b));
        }
        

        var result = foundMultiplier.Select(x => x.Item1 * x.Item2).Sum();

        Result(result);
        
    }
}