using System;
using System.Collections.Generic;

public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.ChildList = new List<Tree<T>>(children);
    }

    public T Value { get; }
    public List<Tree<T>> ChildList { get; }

    public void Print(int indent = 0)
    {
        this.Print(this, indent);
    }

    private void Print(Tree<T> node, int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}{node.Value}");

        foreach (Tree<T> child in node.ChildList)
        {
            child.Print(indent + 2);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);
        foreach (var child in this.ChildList)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();
        this.DFS(this, result);
        return result;
    }

    private void DFS(Tree<T> node, List<T> result)
    {
        foreach (Tree<T> child in node.ChildList)
        {
            this.DFS(child, result);
        }

        result.Add(node.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var result = new List<T>();
        var queue = new Queue<Tree<T>>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result.Add(current.Value);

            foreach (Tree<T> child in current.ChildList)
            {
                queue.Enqueue(child);
            }
        }

        return result;
    }
}