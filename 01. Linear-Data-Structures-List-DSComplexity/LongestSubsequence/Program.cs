using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();

        var number = 0;
        var count = 1;
        var currentCount = 1;

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (numbers[i] == numbers[i + 1])
            {
                currentCount++;
            }
            else
            {
                currentCount = 1;
            }

            if (currentCount > count)
            {
                count = currentCount;
                number = numbers[i];
            }
        }

        var result = new List<int>();
        for (int j = 0; j < count; j++)
        {
            result.Add(number);
        }

        Console.WriteLine(string.Join(" ", result));
    }
}