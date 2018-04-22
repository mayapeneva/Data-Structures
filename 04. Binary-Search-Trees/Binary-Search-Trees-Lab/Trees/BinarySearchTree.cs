using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    public BinarySearchTree()
    {
        this.Root = null;
    }

    public Node Root { get; private set; }

    public BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T value)
    {
        this.Root = this.Insert(this.Root, value);
    }

    private Node Insert(Node node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        var compare = node.Value.CompareTo(value);
        if (compare > 0)
        {
            node.Left = this.Insert(node.Left, value);
        }
        else
        {
            node.Right = this.Insert(node.Right, value);
        }

        return node;
    }

    public bool Contains(T value)
    {
        var currentNode = this.Root;
        while (currentNode != null)
        {
            var compare = currentNode.Value.CompareTo(value);
            if (compare > 0)
            {
                currentNode = currentNode.Left;
            }
            else if (compare < 0)
            {
                currentNode = currentNode.Right;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public void DeleteMin()
    {
        if (this.Root == null)
        {
            return;
        }

        if (this.Root.Left == null && this.Root.Right == null)
        {
            this.Root = null;
        }

        Node parent = null;
        var current = this.Root;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        parent.Left = current.Right ?? null;
    }

    public BinarySearchTree<T> Search(T item)
    {
        var currentNode = this.Root;
        while (currentNode != null)
        {
            var compare = currentNode.Value.CompareTo(item);
            if (compare > 0)
            {
                currentNode = currentNode.Left;
            }
            else if (compare < 0)
            {
                currentNode = currentNode.Right;
            }
            else
            {
                return new BinarySearchTree<T>(currentNode);
            }
        }

        return null;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var result = new List<T>();
        this.Range(this.Root, result, startRange, endRange);
        return result;
    }

    private void Range(Node node, List<T> result, T start, T end)
    {
        if (node == null)
        {
            return;
        }

        var compareLow = node.Value.CompareTo(start);
        var compareHigh = node.Value.CompareTo(end);

        if (compareLow > 0)
        {
            this.Range(node.Left, result, start, end);
        }

        if (compareLow >= 0 && compareHigh <= 0)
        {
            result.Add(node.Value);
        }

        if (compareHigh < 0)
        {
            this.Range(node.Right, result, start, end);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.Root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;
        }

        public T Value { get; private set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
    }
}