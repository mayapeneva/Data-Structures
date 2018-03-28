using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            var number = numbers[i];
            var count = numbers.Count(t => number == t);

            if (count % 2 != 0)
            {
                numbers.RemoveAll(n => n == number);
                i--;
            }
        }

        Console.WriteLine(string.Join(" ", numbers));
    }
}