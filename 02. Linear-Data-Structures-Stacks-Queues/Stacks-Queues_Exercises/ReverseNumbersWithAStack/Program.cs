using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var input = Console.ReadLine().Split().Select(int.Parse).ToArray();

        var result = new Stack<int>();
        foreach (int number in input)
        {
            result.Push(number);
        }

        for (int j = 0; j < input.Length; j++)
        {
            Console.Write($"{result.Pop()} ");
        }
        Console.WriteLine();
    }
}