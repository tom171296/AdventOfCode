namespace AdventOfCode2024.Days7
{
    public class Day07 : AbstractDay
    {
        private long EvaluateExpression(List<long> numbers, List<string> operators)
        {
            var result = numbers[0];
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == "+")
                    result += numbers[i + 1];
                else if (operators[i] == "||")
                    // concat the next number to the current number
                    result = long.Parse(result.ToString() + numbers[i + 1]);
                else
                    result *= numbers[i + 1];
            }
            return result;
        }

        public override void Execute()
        {
            var input = File.ReadAllLines("input.txt");
            var part1 = 0L;
            var part2 = 0L;

            var possibleOperators = new List<string> { "+", "*", "||" };

            foreach (var line in input)
            {
                var parts = line.Split(": ");
                var total = long.Parse(parts[0]);
                var values = parts[1].Split(" ").Select(long.Parse).ToList();

                // Generate all possible operator combinations
                var operatorCount = values.Count - 1;
                var combinations = Enumerable.Range(0, (int)Math.Pow(possibleOperators.Count, operatorCount))
                    .Select(i => Enumerable.Range(0, operatorCount)
                        .Select(pos => possibleOperators[i / (int)Math.Pow(possibleOperators.Count, pos) % possibleOperators.Count])
                        .ToList());

                foreach (var operators in combinations)
                {
                    if (EvaluateExpression(values, operators) == total)
                    {
                        part1 += total;
                        break;
                    }
                }
            }

            Result(part1);
        }
    }
}