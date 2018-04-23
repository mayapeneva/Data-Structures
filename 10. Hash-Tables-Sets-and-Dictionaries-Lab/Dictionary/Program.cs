using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var result = new HashDictionary<char, int>();

        var input = Console.ReadLine().ToCharArray();
        foreach (var symbol in input)
        {
            if (!result.Contains(symbol))
            {
                result[symbol] = 0;
            }

            result[symbol]++;
        }

        foreach (var item in result.OrderBy(s => s.Key))
        {
            Console.WriteLine($"{item.Key}: {item.Value} time/s");
        }
    }
}