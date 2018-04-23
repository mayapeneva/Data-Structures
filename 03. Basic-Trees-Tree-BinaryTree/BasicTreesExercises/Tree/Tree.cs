using System;
using System.Collections.Generic;

internal class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.ChildList = new List<Tree<T>>(children);
        foreach (Tree<T> child in this.ChildList)
        {
            child.Parent = this;
        }
    }

    public T Value { get; }
    public Tree<T> Parent { get; set; }
    public List<Tree<T>> ChildList { get; }

    public void PrintTree(int indent = 0)
    {
        this.PrintByNode(this, indent);
    }

    private void PrintByNode(Tree<T> node, int indent)
    {
        Console.WriteLine($"{new string(' ', indent)}{node.Value}");

        foreach (var child in node.ChildList)
        {
            PrintByNode(child, indent + 2);
        }
    }
}