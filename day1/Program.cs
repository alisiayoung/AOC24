using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Input file path
        string filePath = "input.txt";

        // Lists to store the left and right numbers
        List<int> leftNumbers = new List<int>();
        List<int> rightNumbers = new List<int>();

        ReadFile(filePath, leftNumbers, rightNumbers);

        leftNumbers.Sort();
        rightNumbers.Sort();

        int totalDistance = GetTotalDistance(leftNumbers, rightNumbers);
        Console.WriteLine($"Total distance: {totalDistance}");
        Console.WriteLine($"Similarity score: {GetSimilarityScore(leftNumbers, rightNumbers)}");
    }

    static void ReadFile(string filePath, List<int> leftNumbers, List<int> rightNumbers)
    {

        foreach (string line in File.ReadLines(filePath))
        {
            string[] parts = line.Split(new string[] { "   " }, StringSplitOptions.None);

            if (parts.Length == 2)
            {
                if (int.TryParse(parts[0], out int left) && int.TryParse(parts[1], out int right))
                {
                    leftNumbers.Add(left);
                    rightNumbers.Add(right);
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {line}");
                }
            }
            else
            {
                Console.WriteLine($"Invalid line format: {line}");
            }
        }
    }

    static int GetTotalDistance(List<int> leftNumbers, List<int> rightNumbers)
    {
        int totalDistance = 0;

        for (int i = 0; i < leftNumbers.Count; i++)
        {
            if (leftNumbers[i] < rightNumbers[i])
            {
                totalDistance += rightNumbers[i] - leftNumbers[i];
            }
            else
            {
                totalDistance += leftNumbers[i] - rightNumbers[i];
            }
        }

        return totalDistance;
    }

    static int GetSimilarityScore(List<int> leftNumbers, List<int> rightNumbers)
    {
        int count = 0;
        int similarityScore = 0;

        for (int i = 0; i < leftNumbers.Count; i++)
        {
            count = 0;
            for (int j = 0; j < rightNumbers.Count; j++)
            {
                if (leftNumbers[i] == rightNumbers[j])
                {
                    count++;
                }
            }

            similarityScore += count * leftNumbers[i];
        }

        return similarityScore;
    }
}
