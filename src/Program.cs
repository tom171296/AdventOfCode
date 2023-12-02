using System.Diagnostics;
using AdventOfCode2024.Days;

var day = new Day2();

var stopwatch = new Stopwatch();
stopwatch.Start();

day.Execute();

stopwatch.Stop();
Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
