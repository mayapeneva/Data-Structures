using System;

public class LinkedStack<T>
{
    private Node firstNode;

    public LinkedStack()
    {
        this.Count = 0;
    }

    public int Count { get; private set; }

    public void Push(T element)
    {
        var secondNode = this.firstNode;
        this.firstNode = new Node(element);
        this.firstNode.NextNode = secondNode;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        var nodeToReturn = this.firstNode;
        this.firstNode = this.firstNode.NextNode;
        this.Count--;
        return nodeToReturn.Value;
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
        public Node(T value, Node nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public Node NextNode { get; set; }

        public T Value { get; }
    }
}