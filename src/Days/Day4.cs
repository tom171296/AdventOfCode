namespace AdventOfCode2024.Days;

public class Day4 : AbstractDay
{
    public override void Execute()
    {
        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53

        var dictionary = new Dictionary<int, int>();

        var input = GetInput();
        var cards = new int[input.Length];
        Array.Fill(cards, 1);

        var part1 = 0;
        var part2 = 0;

        for (var i = 0; i < input.Length; i++){
            var card = input[i];

            var totalForCard = 0;
            var numbers = card.Split(':');
            var numbersSplit = numbers[1].Split('|');
            
            var winningNumbers = numbersSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var ownNumbers = numbersSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var matches = winningNumbers.Intersect(ownNumbers);

            for (var j = 1; j <= matches.Count(); j++)
            {
                if (totalForCard == 0){
                    totalForCard += 1;
                }
                else {
                    totalForCard *= 2;
                }

                if (i + j < input.Length)
                {
                    if(dictionary.TryGetValue(i+j, out int x))
                    {
                        dictionary[i+j] = x + 1;
                    } else {
                        dictionary.Add(i+j, 1);
                    }
                }
            }

            for (var s = i + 1; s < i + 1 + matches.Count() && s < input.Length; s++)
            {
                cards[s] += cards[i];
            }

            part1 += totalForCard;
        }

        for (var i = 0; i < cards.Length; i++)
        {
            part2 += cards[i];
        }

        Result(part1);
        Result(part2);
    }
}