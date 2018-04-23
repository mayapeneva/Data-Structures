using System;

public class Program
{
    public static void Main()
    {
        var set = new OrderedSet<int>();
        set.Add(17);
        set.Add(9);
        set.Add(12);
        set.Add(19);
        set.Add(6);
        set.Add(25);

        Console.WriteLine(set.Count);
        set.Remove(12);
        Console.WriteLine(set.Count);

        Console.WriteLine(set.Contains(12));
        Console.WriteLine(set.Contains(6));

        foreach (var item in set)
        {
            Console.WriteLine(item);
        }
    }
}