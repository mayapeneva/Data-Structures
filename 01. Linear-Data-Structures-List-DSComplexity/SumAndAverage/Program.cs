using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        List<int> numbers = Console.ReadLine().Split().Select(int.Parse).ToList();

        Console.Write($"Sum={numbers.Sum()}; Average={numbers.Average():f2}");
    }
}