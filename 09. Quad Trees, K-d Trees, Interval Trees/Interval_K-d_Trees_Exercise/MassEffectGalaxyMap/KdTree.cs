using System;
using System.Collections.Generic;
using System.Linq;

public class KdTree
{
    private const int K = 2;

    private Node root;

    public class Node
    {
        public Node(Star star)
        {
            this.Star = star;
        }

        public Star Star { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root => this.root;

    public bool Contains(Star point)
    {
        if (this.root == null)
        {
            return false;
        }

        return this.Contains(this.root, point, 0);
    }

    private bool Contains(Node node, Star point, int depth)
    {
        if (node.Star.Equals(point))
        {
            return true;
        }

        if (depth % K == 0)
        {
            var compare = node.Star.X.CompareTo(point.X);
            if (node.Left != null && compare > 0)
            {
                return this.Contains(node.Left, point, depth + 1);
            }

            if (node.Right != null && compare <= 0)
            {
                return this.Contains(node.Right, point, depth + 1);
            }
        }
        else
        {
            var cmp = node.Star.Y.CompareTo(point.Y);
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

    public void Insert(Star point)
    {
        this.root = this.Insert(this.root, point, 0);
    }

    private Node Insert(Node node, Star point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }

        if (depth % K == 0)
        {
            var compare = node.Star.X.CompareTo(point.X);
            if (node.Left != null && compare > 0)
            {
                node.Left = this.Insert(node.Left, point, depth + 1);
            }

            if (node.Right != null && compare <= 0)
            {
                node.Right = this.Insert(node.Right, point, depth + 1);
            }
        }
        else
        {
            var cmp = node.Star.Y.CompareTo(point.Y);
            if (node.Left != null && cmp > 0)
            {
                node.Left = this.Insert(node.Left, point, depth + 1);
            }

            if (node.Right != null && cmp <= 0)
            {
                node.Right = this.Insert(node.Right, point, depth + 1);
            }
        }

        return node;
    }

    public int EachInOrder(Func<Star, int> function)
    {
        return this.EachInOrder(this.root, function);
    }

    private int EachInOrder(Node node, Func<Star, int> function)
    {
        var count = 0;
        if (node == null)
        {
            return count;
        }

        count += this.EachInOrder(node.Left, function);
        count += function(node.Star);
        count += this.EachInOrder(node.Right, function);

        return count;
    }

    public void BuildFromList(List<Star> starList)
    {
        this.root = this.Build(starList);
    }

    private Node Build(List<Star> starList, int depth = 0)
    {
        if (starList.Count == 0)
        {
            return null;
        }

        //var axis = depth % 2;
        //if (axis == 0)
        //{
        //    starList.Sort((a, b) => a.X.CompareTo(b.X));
        //}
        //else
        //{
        //    starList.Sort((a, b) => a.Y.CompareTo(b.Y));
        //}

        starList.Sort((a, b) => a.X.CompareTo(b.X));
        if (starList.Count == 1)
        {
            var node = new Node(starList[0]);
            starList.RemoveAt(0);
            return node;
        }

        var median = starList.Count / 2;
        var newNode = new Node(starList[median]);
        starList.RemoveAt(median);

        var leftList = starList.Take(median).ToList();
        if (leftList.Count > 0)
        {
            newNode.Left = this.Build(leftList, depth + 1);
        }

        var rightList = starList.Skip(median).ToList();
        if (rightList.Count > 0)
        {
            newNode.Right = this.Build(rightList, depth + 1);
        }

        return newNode;
    }
}