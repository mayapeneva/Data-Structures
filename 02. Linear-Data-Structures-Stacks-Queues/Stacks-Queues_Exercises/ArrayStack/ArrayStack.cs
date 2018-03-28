using System;
using System.Linq;

public class ArrayStack<T>
{
    private const int InitialCapacity = 16;

    private T[] arrayStack;

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.arrayStack = new T[capacity];
        this.Count = 0;
    }

    public int Count { get; private set; }

    public void Push(T element)
    {
        if (this.Count >= this.arrayStack.Length)
        {
            this.Grow();
        }
        this.arrayStack[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var element = this.arrayStack[this.Count - 1];
        this.arrayStack[this.Count - 1] = default(T);
        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        var newArrStack = new T[this.Count];
        Array.Copy(this.arrayStack, newArrStack, this.Count);
        return newArrStack.Reverse().ToArray();
    }

    private void Grow()
    {
        var newArrStack = new T[this.arrayStack.Length * 2];
        Array.Copy(this.arrayStack, newArrStack, this.Count);
        this.arrayStack = newArrStack;
    }
}