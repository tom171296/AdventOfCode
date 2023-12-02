
using System.Diagnostics;

namespace AdventOfCode2024.Days;
public class Day2 : AbstractDay
{

    private readonly int RedLength = "red".Length;
    private readonly int GreenLength = "green".Length;
    private readonly int BlueLength = "blue".Length;
    public override void Execute()
    {
        var input = GetInput();
        //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var deel1 = 0;
        var deel2 = 0;

        foreach (var line in input)
        {
            var cursor = 5;
            var game = ReadNumber(line, ref cursor);

            cursor += 2;
            var redMaxValue = 1;
            var greenMaxValue = 1;
            var blueMaxValue = 1;

            while (cursor < line.Length)
            {
                var colorNumber = ReadNumber(line, ref cursor);
                cursor++;
                switch (line[cursor])
                {
                    case 'r':
                        if (redMaxValue < colorNumber)
                            redMaxValue = colorNumber;
                        cursor += RedLength;
                        break;

                    case 'g':
                        if (greenMaxValue < colorNumber)
                            greenMaxValue = colorNumber;
                        cursor += GreenLength;
                        break;

                    case 'b':
                        if (blueMaxValue < colorNumber)
                            blueMaxValue = colorNumber;
                        cursor += BlueLength;
                        break;
                }

                // move to next number
                cursor += 2;
            }

            if (redMaxValue <= 12
                && greenMaxValue <= 13
                && blueMaxValue <= 14)
            {
                deel1 += game;
            }

            deel2 += redMaxValue * greenMaxValue * blueMaxValue;
        }
        stopwatch.Stop();

        Result(deel1);
        Result(deel2);
        Console.WriteLine($"Execution Time: {stopwatch.ElapsedTicks} ticks");

    }

    private int ReadNumber(string text, ref int cursor)
    {
        var number = 0;
        while(text[cursor] >= '0'
            && text[cursor] <= '9')
        {
            number = number * 10 + (text[cursor] - '0');
            cursor++;
        }

        return number;
    }
}