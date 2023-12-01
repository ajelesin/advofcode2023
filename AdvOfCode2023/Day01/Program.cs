using System.Collections.Generic;

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

    private static void f1(IEnumerable<string> lines)
    {
        var total = 0;
        foreach (var line in lines)
        {
            int first = 0, last = 0;

            var i = 0;
            while (!char.IsDigit(line[i]))
                i+=1;
            first = line[i] - 48;

            i = line.Length - 1;
            while (!char.IsDigit(line[i]))
                i-=1;
            last = line[i] - 48;

            var num = first * 10 + last;
            total += num;
        }

        Console.WriteLine(total);
    }

    private static void f2(IEnumerable<string> lines)
    {
        var total = 0;
        foreach (var line in lines)
        {
            var first = digitmap[Find(line, digits)];
            var last = digitmap[Reverse(Find(
                Reverse(line),
                digits.Select(Reverse).ToArray()
                ))];

            var num = first * 10 + last;
            Console.WriteLine(num);
            total += num;
        }

        Console.WriteLine(total);
    }

    private static string Reverse(string value)
    {
        return new string(value.Reverse().ToArray());
    }

    private static string Find(string line, string[] initMatch)
    {
        for (var i = 0; i < line.Length; i += 1)
        {
            var j = 0;
            var match = initMatch;

            while (true)
            {
                match = Match(line[i + j], j, match);

                if (match.Length == 0)
                {
                    break;
                }

                if (match.Length == 1 && match[0].Length - 1 == j)
                {
                    return match[0];
                }

                j += 1;
            }
        }

        throw new Exception("wtf");
    }

    private static string[] Match(char c, int index, string[] candidates)
    {
        var cand = new List<string>();

        foreach (var candidate in candidates)
        {
            if (candidate.Length > index && candidate[index] == c)
                cand.Add(candidate);
        }

        return cand.ToArray();
    }

    private static readonly string[] digits = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    private static readonly Dictionary<string, int> digitmap = new Dictionary<string, int>
    {
        { "1", 1 },
        { "2", 2 },
        { "3", 3 },
        { "4", 4 },
        { "5", 5 },
        { "6", 6 },
        { "7", 7 },
        { "8", 8 },
        { "9", 9 },
        { "0", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };
}