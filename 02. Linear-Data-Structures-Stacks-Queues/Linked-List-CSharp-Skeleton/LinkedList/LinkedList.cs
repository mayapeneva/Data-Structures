using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    private Node head;
    private Node tail;

    public LinkedList()
    {
        this.head = null;
        this.tail = null;
        this.Count = 0;
    }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var newNode = new Node(item);
        if (this.Count == 0)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            newNode.Next = this.head;
            this.head = newNode;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        var newNode = new Node(item);
        if (this.Count == 0)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            this.tail.Next = newNode;
            this.tail = newNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var nodeToRemove = this.head;
        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
        }

        this.Count--;
        return nodeToRemove.Value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var nodeToRemove = this.tail;
        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            var parentNode = this.head;
            while (parentNode.Next != this.tail)
            {
                parentNode = parentNode.Next;
            }
            parentNode.Next = null;
            this.tail = parentNode;
        }

        this.Count--;
        return nodeToRemove.Value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var startNode = this.head;
        while (startNode != null)
        {
            yield return startNode.Value;
            startNode = startNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }
        public Node Next { get; set; }
    }
}