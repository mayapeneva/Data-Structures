using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private const int InitialCapacity = 2;

    public T[] list;
    public int Capacity { get; private set; }
    public int Count { get; private set; }

    public ReversedList()
    {
        this.Capacity = InitialCapacity;
        this.list = new T[InitialCapacity];
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.list[this.Count - index - 1];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.list[this.Count - index - 1] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count >= this.Capacity)
        {
            this.Grow();
        }

        this.list[this.Count] = item;
        this.Count++;
    }

    private void Grow()
    {
        var newList = new T[this.Capacity * 2];
        Array.Copy(this.list, newList, this.Count);
        this.Capacity *= 2;
        this.list = newList;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        var element = this[index];
        this.ShiftLeft(this.Count - index - 1);
        this.Count--;
        return element;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            this.list[i] = this.list[i + 1];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}