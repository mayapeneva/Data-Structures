using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
        var n = numbers[0];
        var m = numbers[1];

        var queue = new Queue<Item>();
        queue.Enqueue(new Item(n));
        while (queue.Count > 0)
        {
            var item = queue.Dequeue();
            if (item.Value == m)
            {
                item = PrintSequence(n, item);
                return;
            }

            if (item.Value < m)
            {
                queue.Enqueue(new Item(item.Value + 1, item));
                queue.Enqueue(new Item(item.Value + 2, item));
                queue.Enqueue(new Item(item.Value * 2, item));
            }
        }
    }

    private static Item PrintSequence(int n, Item item)
    {
        var result = new List<int>();
        while (item.Value != n)
        {
            result.Add(item.Value);
            item = item.PrevItem;
        }
        result.Add(n);
        result.Sort();

        Console.WriteLine(string.Join(" -> ", result));
        return item;
    }

    private class Item
    {
        public Item(int value)
        {
            this.Value = value;
            this.PrevItem = null;
        }

        public Item(int value, Item prevItem)
            : this(value)
        {
            this.PrevItem = prevItem;
        }

        public int Value { get; }
        public Item PrevItem { get; }
    }
}