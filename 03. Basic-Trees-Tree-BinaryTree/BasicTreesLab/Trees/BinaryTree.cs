using System;

public class BinaryTree<T>
{
    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
    }

    public T Value { get; }
    public BinaryTree<T> LeftChild { get; }
    public BinaryTree<T> RightChild { get; }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        this.PrintPreOrder(this, indent);
    }

    private void PrintPreOrder(BinaryTree<T> node, int indent)
    {
        if (node != null)
        {
            Console.WriteLine($"{new string(' ', indent)}{node.Value}");
            this.PrintPreOrder(node.LeftChild, indent + 2);
            this.PrintPreOrder(node.RightChild, indent + 2);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this, action);
    }

    private void EachInOrder(BinaryTree<T> node, Action<T> action)
    {
        if (node != null)
        {
            this.EachInOrder(node.LeftChild, action);
            action(node.Value);
            this.EachInOrder(node.RightChild, action);
        }
    }

    public void EachPostOrder(Action<T> action)
    {
        this.EachPostOrder(this, action);
    }

    private void EachPostOrder(BinaryTree<T> node, Action<T> action)
    {
        if (node != null)
        {
            this.EachPostOrder(node.LeftChild, action);
            this.EachPostOrder(node.RightChild, action);
            action(node.Value);
        }
    }
}