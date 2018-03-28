using System;

public class LinkedQueue<T>
{
    private Node firstNode;
    private Node lastNode;

    public LinkedQueue()
    {
        this.Count = 0;
    }

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        var newNode = new Node(element);
        if (this.Count == 0)
        {
            this.firstNode = this.lastNode = newNode;
        }
        else if (this.Count == 1)
        {
            this.lastNode = this.firstNode.NextNode = newNode;
            this.lastNode.PrevNode = this.firstNode;
        }
        else
        {
            var secondLastNode = this.lastNode;
            this.lastNode.NextNode = newNode;
            this.lastNode = newNode;
            this.lastNode.PrevNode = secondLastNode;
        }

        this.Count++;
    }

    public T Dequeue()
    {
        var node = this.firstNode;
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        else if (this.Count == 1)
        {
            this.firstNode = this.lastNode = this.firstNode.NextNode = this.lastNode.PrevNode = null;
        }
        else if (this.Count == 2)
        {
            this.firstNode = this.lastNode;
            this.firstNode.NextNode = this.firstNode.PrevNode = this.lastNode.NextNode = this.lastNode.PrevNode = null;
        }
        else
        {
            this.firstNode = this.firstNode.NextNode;
            this.firstNode.PrevNode = null;
        }

        this.Count--;
        return node.Value;
    }

    public T[] ToArray()
    {
        var collection = new T[this.Count];
        var node = this.firstNode;
        for (int i = 0; i < this.Count; i++)
        {
            collection[i] = node.Value;
            node = node.NextNode;
        }

        return collection;
    }

    private class Node
    {
        public Node(T value, Node nextNode = null, Node prevNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
            this.PrevNode = prevNode;
        }

        public Node PrevNode { get; set; }
        public Node NextNode { get; set; }

        public T Value { get; }
    }
}