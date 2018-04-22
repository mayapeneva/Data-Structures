using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp(this.Count - 1);
    }

    private void HeapifyUp(int childIndex)
    {
        var parentIndex = (childIndex - 1) / 2;

        if (parentIndex < 0)
        {
            return;
        }

        var compare = this.heap[parentIndex].CompareTo(this.heap[childIndex]);
        if (compare < 0)
        {
            var temp = this.heap[parentIndex];
            this.heap[parentIndex] = this.heap[childIndex];
            this.heap[childIndex] = temp;

            this.HeapifyUp(parentIndex);
        }
    }

    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var element = this.heap[0];
        this.heap[0] = this.heap[this.Count - 1];
        this.heap.RemoveAt(this.Count - 1);
        this.HeapifyDown(0);

        return element;
    }

    private void HeapifyDown(int parentIndex)
    {
        if (parentIndex >= this.Count / 2)
        {
            return;
        }

        var childIndex = (parentIndex * 2) + 1;
        if (childIndex + 1 < this.Count)
        {
            var compare = this.heap[childIndex].CompareTo(this.heap[childIndex + 1]);
            childIndex = compare > 0 ? childIndex : childIndex + 1;
        }

        var compare2 = this.heap[parentIndex].CompareTo(this.heap[childIndex]);
        if (compare2 >= 0)
        {
            return;
        }

        var temp = this.heap[parentIndex];
        this.heap[parentIndex] = this.heap[childIndex];
        this.heap[childIndex] = temp;
        this.HeapifyDown(childIndex);
    }
}