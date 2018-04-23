using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private Node root;

    public BinarySearchTree()
    {
    }

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    public int Count()
    {
        return this.Count(this.root);
    }

    private int Count(Node node)
    {
        var count = 0;
        if (node == null)
        {
            return count;
        }

        count += this.Count(node.Left);
        count++;
        count += this.Count(node.Right);

        return count;
    }

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        return node;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
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

    public void Delete(T element)
    {
        if (this.root == null || !this.Contains(element))
        {
            throw new InvalidOperationException();
        }

        this.root = this.Delete(this.root, element);
    }

    private Node Delete(Node node, T element)
    {
        if (node == null)
        {
            return null;
        }

        var compare = node.Value.CompareTo(element);
        if (compare > 0)
        {
            node.Left = this.Delete(node.Left, element);
        }
        else if (compare < 0)
        {
            node.Right = this.Delete(node.Right, element);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            if (node.Right == null)
            {
                return node.Left;
            }

            var leftMostNode = this.LeftMost(node.Right);
            node.Value = leftMostNode.Value;
            node.Right = this.Delete(node.Right, leftMostNode.Value);
        }

        return node;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMax(this.root);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null)
        {
            return node.Left;
        }

        node.Right = this.DeleteMax(node.Right);

        return node;
    }

    private Node LeftMost(Node node)
    {
        var currentNode = node;
        while (currentNode.Left != null)
        {
            currentNode = currentNode.Left;
        }

        return currentNode;
    }

    public int Rank(T element)
    {
        return this.Rank(this.root, element);
    }

    private int Rank(Node node, T element)
    {
        if (node == null)
        {
            return 0;
        }

        var compare = node.Value.CompareTo(element);
        if (compare > 0)
        {
            return this.Rank(node.Left, element);
        }

        if (compare < 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(node.Right, element);
        }

        return this.Count(node.Left);
    }

    public T Select(int rank)
    {
        if (this.root == null || rank > this.Count(this.root) - 2)
        {
            throw new InvalidOperationException();
        }

        return this.Select(this.root, rank);
    }

    private T Select(Node node, int number)
    {
        if (node == null)
        {
            return default(T);
        }

        var count = this.Count(node.Left);
        if (count > number)
        {
            return this.Select(node.Left, number);
        }

        if (count < number)
        {
            return this.Select(node.Right, number - (count + 1));
        }

        return node.Value;
    }

    public T Ceiling(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        return this.Select(this.Rank(element) + 1);
    }

    public T Floor(T element)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        return this.Select(this.Rank(element) - 1);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}