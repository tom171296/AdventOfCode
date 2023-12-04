namespace AdventOfCode2024.Days;

using GearPoint = (int X, int Y);
public class Day3 : AbstractDay
{
    public override void Execute()
    {
        var input = GetInput();

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var schemeHeight = input.Length;
        var schemeWidth = input[0].Length;

        var part1 = 0;
        var part2 = 0;

        var gears = new Dictionary<GearPoint, List<int>>();

        for (var y = 0; y < schemeHeight; y++)
        {
            var numberLength = 0;
            var number = 0;

            for (var x = 0; x < schemeWidth; x++)
            {
                // Scan for numbers
                if (char.IsAsciiDigit(input[y][x]) && x < schemeWidth)
                {
                    numberLength++;
                    number *= 10;
                    number += input[y][x] - '0';
                }

                if (number > 0 && (x == schemeWidth - 1 || !char.IsAsciiDigit(input[y][x + 1])))
                {
                    for (var boxY = y - 1; boxY <= y + 1; boxY++)
                    {
                        if (boxY < 0 || boxY >= schemeHeight)
                        {
                            continue;
                        }

                        for (var boxX = x - numberLength; boxX <= x + 1; boxX++)
                        {
                            if (boxX < 0 || boxX >= schemeWidth)
                            {
                                continue;
                            }

                            var boxSymbol = input[boxY][boxX];
                            if (boxSymbol != '.' && !char.IsAsciiDigit(boxSymbol))
                            {
                                // Number is attached to a part
                                part1 += number;
                            }

                            if (boxSymbol == '*')
                            {
                                // It's a gear!
                                var point = new GearPoint(boxY, boxX);
                                if (gears.TryGetValue(point, out var value))
                                {
                                    value.Add(number);
                                }
                                else
                                {
                                    gears.Add(point, [number]);
                                }
        
                                goto reset;
                            }
                        }
                    }
                    reset :
                        numberLength = 0;
                        number = 0;
                }
            }
        }

        foreach (var gear in gears)
        {
            if (gear.Value.Count != 2){
                continue;
            }
            part2 += gear.Value[0] * gear.Value[1];
        }

        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        Result($"part1: {part1}");
        Result($"part2: {part2}");
    }    
}

