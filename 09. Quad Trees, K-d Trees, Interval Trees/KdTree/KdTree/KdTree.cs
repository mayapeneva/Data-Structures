using System;

public class KdTree
{
    private const int K = 2;

    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root => this.root;

    public bool Contains(Point2D point)
    {
        if (this.root == null)
        {
            return false;
        }

        return this.Contains(this.root, point, 0);
    }

    private bool Contains(Node node, Point2D point, int depth)
    {
        if (node.Point.Equals(point))
        {
            return true;
        }

        var compare = depth % K;
        if (compare == 0)
        {
            var cmp = node.Point.X.CompareTo(point.X);
            if (node.Left != null && cmp > 0)
            {
                return this.Contains(node.Left, point, depth + 1);
            }

            if (node.Right != null && cmp <= 0)
            {
                return this.Contains(node.Right, point, depth + 1);
            }
        }
        else
        {
            var cmp = node.Point.Y.CompareTo(point.Y);
            if (node.Left != null && cmp > 0)
            {
                return this.Contains(node.Left, point, depth + 1);
            }

            if (node.Right != null && cmp <= 0)
            {
                return this.Contains(node.Right, point, depth + 1);
            }
        }

        return false;
    }

    public void Insert(Point2D point)
    {
        this.root = this.Insert(this.root, point, 0);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }

        var compare = depth % K;
        if (compare == 0)
        {
            var cmp = node.Point.X.CompareTo(point.X);
            if (cmp > 0)
            {
                node.Left = this.Insert(node.Left, point, depth + 1);
            }
            else
            {
                node.Right = this.Insert(node.Right, point, depth + 1);
            }
        }
        else
        {
            var cmp = node.Point.Y.CompareTo(point.Y);
            if (cmp > 0)
            {
                node.Left = this.Insert(node.Left, point, depth + 1);
            }
            else
            {
                node.Right = this.Insert(node.Right, point, depth + 1);
            }
        }

        return node;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}