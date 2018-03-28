using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
        var result = new Dictionary<int, int>();

        int number;
        var count = 1;

        if (numbers.Count > 1)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                number = numbers[i];
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (number == numbers[j])
                    {
                        count++;
                    }
                }

                result[number] = count;
                count = 1;
                numbers.RemoveAll(n => n == number);
                i--;
            }
        }
        else
        {
            result[numbers[0]] = 1;
        }

        foreach (var item in result.OrderBy(n => n.Key))
        {
            Console.WriteLine($"{item.Key} -> {item.Value} times");
        }
    }
}