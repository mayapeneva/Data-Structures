using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root => this.root;

    public int Count()
    {
        var count = 0;
        this.EachInOrder(t => count++);
        return count;
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return this.Search(node.Left, item);
        }

        if (cmp > 0)
        {
            return this.Search(node.Right, item);
        }

        return node;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = this.BalanceTree(node);

        return node;
    }

    public void Delete(T item)
    {
        if (this.root != null)
        {
            this.root = this.Delete(this.root, item);
        }
    }

    private Node<T> Delete(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Delete(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Delete(node.Right, item);
        }
        else
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            if (node.Left == null)
            {
                return node.Right;
            }

            var temp = node;
            node = this.FindMin(temp.Right);
            node.Right = this.DeleteMin(temp.Right);
            node.Left = temp.Left;
        }

        node = this.BalanceTree(node);
        this.UpdateHeight(node);
        return node;
    }

    private Node<T> FindMin(Node<T> node)
    {
        if (node.Left == null)
        {
            return node;
        }

        return this.FindMin(node.Left);
    }

    public void DeleteMin()
    {
        if (this.root != null)
        {
            this.root = this.DeleteMin(this.root);
        }
    }

    private Node<T> DeleteMin(Node<T> node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);

        this.UpdateHeight(node);

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;
    }

    private Node<T> BalanceTree(Node<T> node)
    {
        node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));

        var balance = this.Height(node.Left) - this.Height(node.Right);

        if (balance > 1)
        {
            var childBalance = this.Height(node.Left.Left) - this.Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = this.RotateLeft(node.Left);
            }

            node = this.RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = this.Height(node.Right.Left) - this.Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = this.RotateRight(node.Right);
            }

            node = this.RotateLeft(node);
        }

        return node;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;

        node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
        newRoot.Height = 1 + Math.Max(this.Height(newRoot.Left), this.Height(newRoot.Right));

        return newRoot;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;

        node.Height = 1 + Math.Max(this.Height(node.Left), this.Height(node.Right));
        newRoot.Height = 1 + Math.Max(this.Height(newRoot.Left), this.Height(newRoot.Right));

        return newRoot;
    }

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}