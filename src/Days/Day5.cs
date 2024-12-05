namespace AdventOfCode2024.Days
{
    public class Day05 : AbstractDay
    {
        public override void Execute()
        {

            var input = File.ReadAllLines("input.txt");

            int emptyLineIndex = Array.IndexOf(input, string.Empty);

            string[] orderingLines = input[0..emptyLineIndex];
            OrderingRules orderingRules = new OrderingRules(orderingLines);

            string[] updateLines = input[(emptyLineIndex + 1)..];
            List<Update> updates = updateLines.Select(line => new Update(line)).ToList();

            var part1 = 0;
            var part2 = 0;
            foreach (Update update in updates)
            {
                if (update.IsCorrectlyOrderedAccordingTo(orderingRules))
                {
                    part1 += update.GetMiddlePageNumber();
                }
                else
                {
                    part2 += update.GetMiddlePageNumberAfterOrdering();
                }
            }
            Console.WriteLine($"part 1 = {part1}");
            Console.WriteLine($"part 2 = {part2}");            
        }
    }
}

class OrderingRules
{
    private HashSet<(int,int)> _rules;

    public OrderingRules(string[] orderingLines)
    {
        var rules = from line in orderingLines
                    let pair = line.Split('|').Select(int.Parse)
                    select (pair.ElementAt(0), pair.ElementAt(1));

        _rules = rules.ToHashSet();
    }

    public int OrderOf(int firstPage, int secondPage)
    {
        if (firstPage == secondPage)
        {
            return 0;   // pages are the same
        }
        else if (_rules.Contains((secondPage, firstPage)))
        {
            return 1;   // the second page should come first
        }
        else
        {
            return -1;  // the first Page should come first
        }
    }
}

class Update
{
    private int[] _pageNumbers;
    private int[] _sortedPageNumbers;

    public Update(string line)
    {
        _pageNumbers = line.Split(',').Select(int.Parse).ToArray();
        _sortedPageNumbers = null!;
    }

    public bool IsCorrectlyOrderedAccordingTo(OrderingRules orderingRules)
    {
        _sortedPageNumbers = CopyOf(_pageNumbers);
        Array.Sort(_sortedPageNumbers, orderingRules.OrderOf);

        return _pageNumbers.SequenceEqual(_sortedPageNumbers);
    }

    private int[] CopyOf(int[] original)
    {
        var copy = new int[original.Length];
        original.CopyTo(copy, 0);
        return copy;
    }

    public int GetMiddlePageNumber()
    {
        int middleIndex = _pageNumbers.Length / 2;
        return _pageNumbers[middleIndex];
    }

    public int GetMiddlePageNumberAfterOrdering()
    {
        int middleIndex = _sortedPageNumbers.Length / 2;
        return _sortedPageNumbers[middleIndex];
    }
}