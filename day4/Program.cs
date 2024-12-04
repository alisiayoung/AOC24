using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";
        string[] lines = File.ReadAllLines(filePath);
        
        Regex xmas = new Regex(@"XMAS");
        Regex samx = new Regex(@"SAMX");

        int count = CheckHorizontally(xmas, lines) + CheckHorizontally(samx, lines);
        count += CheckVertically(xmas, lines) + CheckVertically(samx, lines);
        count += CheckDiagonally(xmas, lines) + CheckDiagonally(samx, lines);

        Console.WriteLine(count);
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

        foreach (string diagonal in diagonalLines)
        {
            Console.WriteLine(diagonal);
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
