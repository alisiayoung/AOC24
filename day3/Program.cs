using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string inputPath = "input.txt";
        string input = File.ReadAllText(inputPath);

        Regex regex = new Regex(@"mul\((?<X>\d+),(?<Y>\d+)\)");
        MatchCollection matches = regex.Matches(input);

        int sum = 0;

        foreach (Match match in matches)
        {
            if (match.Success)
            {
                int x = int.Parse(match.Groups["X"].Value);
                int y = int.Parse(match.Groups["Y"].Value);

                sum += x * y;
            }
        }

        Console.WriteLine($"Total sum: {sum}");
    }
}
