public abstract class AbstractDay 
{
    public abstract void Execute();

    public string[] GetInput() {
        return File.ReadAllLinesAsync("input.txt").Result;
    }

    public void Result(object result) {
        Console.WriteLine($"Result: {result}");
    }
}