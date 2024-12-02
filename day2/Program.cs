// the most spaghetti spaghetti code can get
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = "input.txt";

        int safeCount = 0;

        foreach(string line in File.ReadLines(filePath))
        {
            int[] levels = line.Split(new string[] { " " }, StringSplitOptions.None)
                .Select(int.Parse)
                .ToArray();

            if (IsReportSafe(levels)) 
            {
                safeCount++;
                continue;
            }

            bool dampenerMakesSafe = false;

            for (int i = 0; i < levels.Length; i++)
            {
                int[] modifiedLevels = levels.Where((_, index) => index != i).ToArray();

                if (IsReportSafe(modifiedLevels))
                {
                    dampenerMakesSafe = true;
                    break;
                }
            }

            if (dampenerMakesSafe)
                safeCount++;
        }

        Console.WriteLine(safeCount);  
    }

    static bool IsReportSafe(int[] levels)
    {
        bool isSafe = true;

        for (int i = 0; i < levels.Length - 1; i++)
        {
            if (levels[i] == levels[i + 1]) 
            {
                isSafe = false;
                break;
            }
            else if (i > 0 && levels[i] < levels[i + 1] && levels[i] < levels[i - 1]) 
            {
                isSafe = false;
                break;
            }
            else if (i > 0 && levels[i] > levels[i + 1] && levels[i] > levels[i - 1])
            {
                isSafe = false;
                break;
            }
            else if (levels[i] > levels[i + 1] && levels[i] - levels[i + 1] > 3)
            {
                isSafe = false;
                break;
            }
            else if (levels[i] < levels[i + 1] && levels[i + 1] - levels[i] > 3)
            {
                isSafe = false;
                break;
            }
        }

        if (isSafe)
            return true;
        else
            return false;
    }
}