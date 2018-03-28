using System;

public class ArrayList<T>
{
    private const int InitialCapacity = 2;
    private T[] arr;
    public int Count { get; private set; }
    public int Capacity { get; private set; }

    public ArrayList()
    {
        this.arr = new T[InitialCapacity];
        this.Capacity = InitialCapacity;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.arr[index];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.arr[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count + 1 >= this.Capacity)
        {
            this.Grow();
        }
        this.arr[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        var item = this[index];
        this.ShiftLeft(index);
        this.Count--;
        return item;
    }

    private void Grow()
    {
        var newArr = new T[this.Capacity * 2];
        this.Capacity *= 2;
        Array.Copy(this.arr, newArr, this.Count);
        this.arr = newArr;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count; i++)
        {
            this.arr[i] = this.arr[i + 1];
        }
    }
}