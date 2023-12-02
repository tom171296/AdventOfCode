using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Days;

public class Day01 : AbstractDay
{
    [Benchmark]
    public override void Execute()
    {
        var lines = GetInput();

        var sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            line = line.Replace("zero", "zer0ero");
            line = line.Replace("one", "on1ne");
            line = line.Replace("two", "tw2wo");
            line = line.Replace("three", "th3hree");
            line = line.Replace("four", "fou4our");
            line = line.Replace("five", "fiv5ive");
            line = line.Replace("six", "si6ix");
            line = line.Replace("seven", "seve7even");
            line = line.Replace("eight", "eigh8ight");
            line = line.Replace("nine", "nin9ine");

            var first = -1;
            var last = -1;

            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] > '0' && line[j] <= '9')
                {
                    var parsedNumber = line[j] - '0';
                    if (first == -1)
                    {
                        first = parsedNumber;
                    }

                    last = parsedNumber;
                }

            }
            
            var number = first * 10 + last;
            sum += number;
        }

        Result(sum);
    }
}