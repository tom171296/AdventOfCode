namespace AdventOfCode2024.Days6;

public class Day06 : AbstractDay
{
    HashSet<Position> visited = new HashSet<Position>();
    HashSet<Position> newObstacles = new HashSet<Position>();
    HashSet<Position> obstacles = new HashSet<Position>();

    Position start = new(0, 0);

    string[] input = File.ReadAllLines("input.txt");

    public override void Execute()
    {
        var part1 = 0;
        var part2 = 0;

        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y].AsSpan();
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '^')
                {
                start = new Position(x, y);
                }
                else if (line[x] == '#')
                {
                obstacles.Add(new Position(x, y));
                }
            }
        }

        Walk(start, Direction.North, []);

        part1 = visited.Count;
        part2 = newObstacles.Count;

        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    bool Walk(Position position, Direction direction, HashSet<Heading> walkedPath, Position? insertedObstacle = null)
    {
        while (true)
        {
            if (insertedObstacle is null)
            {
            visited.Add(position);
            }
            else if (insertedObstacle is not null && walkedPath.Contains(new Heading(position, direction)))
            {
            // We've been here before, in this direction
            return true;
            }

            walkedPath.Add(new Heading(position, direction));

            var nextPosition = NextPosition(position, direction);

            // OOB?
            if (nextPosition.X < 0 ||
                nextPosition.Y < 0 ||
                nextPosition.X >= input[0].Length ||
                nextPosition.Y >= input.Length)
            {
            return false;
            }

            if (obstacles.Contains(nextPosition) || nextPosition == insertedObstacle)
            {
            // Need to turn right
            direction = TurnRight(direction);
            continue;
            }

            if (insertedObstacle is null && !visited.Contains(nextPosition) && nextPosition != start)
            {
            // We are predicting
            // We can't insert an obstacle where we already have traveled
            // We can't insert an obstacle where we started
            // Would a right turn here bring us on a position we already visited?
            if (Walk(position, TurnRight(direction), [.. walkedPath], nextPosition))
            {
                // Yes, mark the next position as a potential obstacle
                newObstacles.Add(nextPosition);
            }
            }

            position = nextPosition;
        }
    }

    static Direction TurnRight(Direction direction) => (Direction)((int)(direction + 1) % 4);

    static Position NextPosition(Position current, Direction direction) =>
        direction switch
        {
            Direction.North => new Position(current.X, current.Y - 1),
            Direction.East => new Position(current.X + 1, current.Y),
            Direction.South => new Position(current.X, current.Y + 1),
            Direction.West => new Position(current.X - 1, current.Y),
            _ => throw new InvalidOperationException()
        };
}

record struct Heading(Position Position, Direction Direction);



enum Direction
{
  North,
  East,
  South,
  West
}

record struct Position(int X, int Y);

