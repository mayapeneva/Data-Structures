using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var input = Console.ReadLine().Split();
        var number = int.Parse(input[0]);
        var desiredNumber = int.Parse(input[1]);

        var queue = new Queue<int>();
        queue.Enqueue(number);

        var currentIndex = 1;
        while (queue.Count > 0)
        {
            var currentNumber = queue.Dequeue();
            queue.Enqueue(currentNumber + 1);
            queue.Enqueue(currentNumber * 2);

            if (currentNumber == desiredNumber)
            {
                Console.WriteLine(currentIndex);
                break;
            }

            currentIndex++;
        }
    }
}