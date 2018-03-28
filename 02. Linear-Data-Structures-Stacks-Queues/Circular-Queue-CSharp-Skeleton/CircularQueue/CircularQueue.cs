using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;

    private T[] array;
    private int head;
    private int tail;
    public int Count { get; private set; }
    public int Capacity { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.Capacity = capacity;
        this.array = new T[this.Capacity];
        this.head = 0;
        this.tail = 0;
        this.Count = 0;
    }

    public void Enqueue(T element)
    {
        var index = (this.tail + this.head) % this.Capacity;

        if (this.Count >= this.Capacity)
        {
            this.Resize();
        }

        this.array[this.tail] = element;
        this.Count++;
        this.tail = (this.tail + 1) % this.Capacity;
    }

    private void Resize()
    {
        var newArray = new T[this.Capacity * 2];
        this.CopyAllElements(newArray);
        this.Capacity *= 2;
        this.array = newArray;
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < this.Count; i++)
        {
            var oldIndex = (i + this.head) % this.Capacity;
            newArray[i] = this.array[oldIndex];
        }
        this.head = 0;
        this.tail = this.Count;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var element = this.array[this.head];
        this.head = (this.head + 1) % this.Capacity;
        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        var newArray = new T[this.Count];
        this.CopyAllElements(newArray);
        return newArray;
    }
}

public class Example
{
    public static void Main()
    {
        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}