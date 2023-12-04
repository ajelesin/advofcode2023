internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("release? (r) ");

        var lines = Console.ReadLine() == "r"
            ? File.ReadLines("..\\..\\..\\input.txt")
            : File.ReadLines("..\\..\\..\\debug.txt");

        f2(lines);
        Console.ReadLine();
    }

    private static void f2(IEnumerable<string> lines)
    {

        var total = 0;
        foreach (var line in lines)
        {
            var gameParts = line.Split(": ");

            var gameIndex = int.Parse(gameParts[0].Split(' ')[1]);
            var games = gameParts[1].Split("; ");

            int red = 0, green = 0, blue = 0;

            foreach (var game in games)
            {
                var gr = AnalGameResult(game);
                if (gr.ContainsKey("red") && gr["red"] > red)
                    red = gr["red"];
                if (gr.ContainsKey("green") && gr["green"] > green)
                    green = gr["green"];
                if (gr.ContainsKey("blue") && gr["blue"] > blue)
                    blue = gr["blue"];
            }

            var p = red * green * blue;
            total += p;
        }

        Console.WriteLine(total);
    }

    private static void f1(IEnumerable<string> lines)
    {
        var possibleValues = new Dictionary<string, int>
        {
            {"red", 12 },
            {"green", 13 },
            {"blue", 14 },
        };

        var total = 0;
        foreach (var line in lines)
        {
            var gameParts = line.Split(": ");

            var gameIndex = int.Parse(gameParts[0].Split(' ')[1]);
            var games = gameParts[1].Split("; ");

            if (AvailGame(games, possibleValues))
                total += gameIndex;
        }

        Console.WriteLine(total);
    }

    private static bool AvailGame(string[] games, Dictionary<string, int> possibleValues)
    {
        foreach (var game in games)
        {
            var gr = AnalGameResult(game);
            foreach (var g in gr)
            {
                if (possibleValues.ContainsKey(g.Key))
                    if (possibleValues[g.Key] < g.Value)
                        return false;
            }
        }

        return true;
    }

    private static Dictionary<string, int> AnalGameResult(string gameResult)
    {
        var result = new Dictionary<string, int>();
        var parts = gameResult.Split(", ");

        foreach (var part in parts)
        {
            var grPart = part.Split(' ');
            result.Add(grPart[1], int.Parse(grPart[0]));
        }

        return result;
    }
}