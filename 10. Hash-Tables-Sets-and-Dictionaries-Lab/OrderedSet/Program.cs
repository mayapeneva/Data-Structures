using System;

public class Program
{
    public static void Main()
    {
        var set = new OrderedSet<int> { 17, 9, 12, 19, 6, 25 };

        foreach (var item in set)
        {
            Console.WriteLine(item);
        }
    }
}