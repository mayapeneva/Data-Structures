using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var number = int.Parse(Console.ReadLine());

        var queue = new Queue<int>();
        queue.Enqueue(number);
        for (int i = 0; i < 17; i++)
        {
            number = queue.Dequeue();
            queue.Enqueue(number + 1);
            queue.Enqueue(number * 2 + 1);
            queue.Enqueue(number + 2);
            Console.Write($"{number}, ");
        }

        var n = queue.Count;
        for (int i = 0; i < n - 3; i++)
        {
            Console.Write($"{queue.Dequeue()}, ");
        }

        Console.Write($"{queue.Dequeue()}");
    }
}