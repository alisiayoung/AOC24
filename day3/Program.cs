using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        bool instructionEnabled = true;

        string inputPath = "input.txt";
        string input = File.ReadAllText(inputPath);

            Regex sectionRegex = new Regex(@"(do\(\))|(don't\(\))|mul\((\d+),(\d+)\)");

            int totalSum = 0;
            bool enabled = true;

            MatchCollection matches = sectionRegex.Matches(input);

            foreach (Match match in matches)
            {
                if (match.Groups[1].Success) // Match for 'do()'
                {
                    enabled = true;
                }
                else if (match.Groups[2].Success) // Match for 'don't()'
                {
                    enabled = false;
                }
                else if (match.Groups[3].Success) // Match for 'mul(x, y)'
                {
                    if (enabled)
                    {
                        int x = int.Parse(match.Groups[3].Value);
                        int y = int.Parse(match.Groups[4].Value);
                        totalSum += x * y;
                    }
                }
            }

            Console.WriteLine(totalSum);
    }
}
