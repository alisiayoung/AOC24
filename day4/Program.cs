using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";
        string[] lines = File.ReadAllLines(filePath);
        char[,] input = new char[lines.Length, lines[0].Length];

        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                input[i, j] = lines[i][j];
            }
        }
        
        Regex xmas = new Regex(@"XMAS");
        Regex samx = new Regex(@"SAMX");

        int count = CheckHorizontally(xmas, lines) + CheckHorizontally(samx, lines);
        count += CheckVertically(xmas, lines) + CheckVertically(samx, lines);
        count += CheckDiagonally(xmas, lines) + CheckDiagonally(samx, lines);

        Console.WriteLine(count);

        CheckPart2(input);
    }

    static int CheckPart2(char[,] input) 
    {
        int count = 0;

        for (int i = 1; i < input.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < input.GetLength(1) - 1; j++)
            {
                if (input[i, j] != 'A') continue;
                if (input[i - 1, j - 1] == 'M' && input[i + 1, j + 1] == 'S' || input[i + 1, j + 1] == 'M' && input[i - 1, j - 1] == 'S') 
                {
                    if (!(input[i - 1, j + 1] == 'M' && input[i + 1, j - 1] == 'S' || input[i + 1, j - 1] == 'M' && input[i - 1, j + 1] == 'S')) continue;
                    count++;
                }
            }
        }

        Console.WriteLine(count);
        return count;
    }

    static int CheckHorizontally(Regex word, string[] lines)
    {
        int count = 0;

        foreach (string line in lines) 
        {
            MatchCollection matches = word.Matches(line);
            count += matches.Count();
        }
        return count;
    }

    static int CheckVertically(Regex word, string[] lines) 
    {
        int count = 0;
        string[] verticalLines = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            foreach (string line in lines)
            {
                verticalLines[i] += line[i];
            }
        }

        foreach (string line in verticalLines)
        {
            MatchCollection matches = word.Matches(line);
            count += matches.Count;
        }
        return count;
    }

    static int CheckDiagonally(Regex word, string[] lines) 
    {
        List<string> diagonalLines = new List<string>();

        int numRows = lines.Length;
        int numCols = lines[0].Length;

        for (int k = 0; k < numRows + numCols - 1; k++)
        {
            string diagonal = "";
            for (int i = 0; i < numRows; i++)
            {
                int j = k - i;
                if (j >= 0 && j < numCols)
                {
                    diagonal += lines[i][j];
                }
            }

            if (diagonal.Length > 0)
                diagonalLines.Add(diagonal);
        }

        // Traverse diagonals starting from the first column
        for (int startCol = 0; startCol < numCols; startCol++)
        {
            string diagonal = "";
            for (int i = 0, j = startCol; i < numRows && j < numCols; i++, j++)
            {
                diagonal += lines[i][j];
            }

            diagonalLines.Add(diagonal);
        }

        // Traverse diagonals starting from the first row (excluding the top-left corner)
        for (int startRow = 1; startRow < numRows; startRow++)
        {
            string diagonal = "";
            for (int i = startRow, j = 0; i < numRows && j < numCols; i++, j++)
            {
                diagonal += lines[i][j];
            }

            diagonalLines.Add(diagonal);
        }

        int count = 0;
        foreach (string line in diagonalLines)
        {
            MatchCollection matches = word.Matches(line);
            count += matches.Count;
        }
        return count;
    }
}
