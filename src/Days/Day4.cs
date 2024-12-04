namespace AdventOfCode2024.Days
{
    public class Day04 : AbstractDay
    {
        Grid grid = null;
        public override void Execute()
        {
            var input = File.ReadAllLines("input.txt");
            // Stopwatch stopwatch = Stopwatch.StartNew();

            grid = new Grid(input);

            var xmas = from location in grid.Locations
                    from direction in Direction.All8
                    where FoundXmasInDirection(location, direction)
                    select 1;
            var part1 = xmas.Count();

            var part2 = grid.Locations.Count(FoundCrossMasAt);

            Console.WriteLine($"part 1 = {part1}");
            Console.WriteLine($"part 2 = {part2}");        
        }

// -------------------------------------------------------

        bool FoundXmasInDirection(Location location, Direction direction, int index = 0)
        {
            return index == 4 ||    // found XMAS
                grid.At(location) == "XMAS"[index] &&
                FoundXmasInDirection(location.Neighbour(direction), direction, index + 1);
        }


        bool FoundCrossMasAt(Location location)
        {
            return grid.At(location) == 'A' &&
                CheckForMandS(location, Direction.NorthWest, Direction.SouthEast) &&
                CheckForMandS(location, Direction.NorthEast, Direction.SouthWest);
        }

        bool CheckForMandS(Location location, Direction direction, Direction opposingDirection)
        {
            char? m = grid.At(location.Neighbour(direction));
            char? s = grid.At(location.Neighbour(opposingDirection));
            return m == 'M' && s == 'S' || m == 'S' && s == 'M';
        }
    }

    public record Direction(int dx, int dy)
    {
        public static Direction West = new Direction(-1, 0);
        public static Direction North = new Direction(0, -1);
        public static Direction East = new Direction(1, 0);
        public static Direction South = new Direction(0, 1);
        public static Direction NorthWest = new Direction(-1, -1);
        public static Direction NorthEast = new Direction(1, -1);
        public static Direction SouthEast = new Direction(1, 1);
        public static Direction SouthWest = new Direction(-1, 1);
        public static Direction[] All8 = { East, NorthEast, North, NorthWest, West, SouthWest, South, SouthEast };

        public Direction OpposingDirection()
        {
            return All8[(Array.IndexOf(All8, this) + 4) % 8];
        }
    }

    public record Location(int X, int Y)
    {
        public Location Neighbour(Direction direction)
        {
            return new Location(X + direction.dx, Y + direction.dy);
        }
    }

    public class Grid
    {
        public int XMax { get; }
        public int YMax { get; }
        public string[] Lines;

        public Grid(string[] lines)
        {
            XMax = lines[0].Length;
            YMax = lines.Length;
            Lines = lines;
        }

        public bool IsInGrid(int x, int y)
        {
            return 0 <= x && x < XMax && 0 <= y && y < YMax;
        }
        public bool IsInGrid(Location location)
        {
            return IsInGrid(location.X, location.Y);
        }

        public char? At(int x, int y)
        {
            return IsInGrid(x, y) ? Lines[y][x] : null;
        }
        public char? At(Location location)
        {
            return At(location.X, location.Y);
        }

        public IEnumerable<Location> Locations
        {
            get
            {
                for (int x = 0; x < XMax; x++)
                {
                    for (int y = 0; y < YMax; y++)
                    {
                        yield return new Location(x, y);
                    }
                };
            }
        }
    }
}