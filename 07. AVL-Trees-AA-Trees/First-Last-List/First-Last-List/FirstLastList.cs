using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private readonly LinkedList<T> collectionByInsertion;
    private readonly OrderedBag<T> collectionByAscending;
    private readonly OrderedBag<T> collectionByDescending;

    public FirstLastList()
    {
        this.collectionByInsertion = new LinkedList<T>();
        this.collectionByAscending = new OrderedBag<T>();
        this.collectionByDescending = new OrderedBag<T>((a, b) => b.CompareTo(a));
    }

    public int Count => this.collectionByInsertion.Count;

    public void Add(T element)
    {
        this.collectionByInsertion.AddLast(element);
        this.collectionByAscending.Add(element);
        this.collectionByDescending.Add(element);
    }

    public void Clear()
    {
        this.collectionByInsertion.Clear();
        this.collectionByAscending.Clear();
        this.collectionByDescending.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (!this.IsInBorders(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = this.collectionByInsertion.First;
        for (int i = 0; i < count; i++)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (!this.IsInBorders(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = this.collectionByInsertion.Last;
        for (int i = 0; i < count; i++)
        {
            yield return current.Value;
            current = current.Previous;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        if (!this.IsInBorders(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.collectionByDescending.Take(count);
    }

    public IEnumerable<T> Min(int count)
    {
        if (!this.IsInBorders(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.collectionByAscending.Take(count);
    }

    public int RemoveAll(T element)
    {
        var count = this.collectionByAscending.RemoveAllCopies(element);
        this.collectionByDescending.RemoveAllCopies(element);

        if (count != 0)
        {
            var current = this.collectionByInsertion.First;
            while (current != null)
            {
                var temp = current;
                current = current.Next;
                if (temp.Value.CompareTo(element) == 0)
                {
                    this.collectionByInsertion.Remove(temp);
                }
            }
        }

        return count;
    }

    private bool IsInBorders(int count)
    {
        return count >= 0 && count <= this.Count;
    }
}