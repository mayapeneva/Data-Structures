using System;
using System.Collections.Generic;
using System.Linq;

public class QuadTree<T> where T : IBoundable
{
    public const int DefaultMaxDepth = 5;

    public readonly int MaxDepth;

    private Node<T> root;

    public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
    {
        this.root = new Node<T>(0, 0, width, height);
        this.Bounds = this.root.Bounds;
        this.MaxDepth = maxDepth;
    }

    public int Count { get; private set; }

    public Rectangle Bounds { get; private set; }

    public void ForEachDfs(Action<List<T>, int, int> action)
    {
        this.ForEachDfs(this.root, action);
    }

    public bool Insert(T item)
    {
        var currentNode = this.root;
        if (item == null || !item.Bounds.IsInside(currentNode.Bounds))
        {
            return false;
        }

        var depth = 1;
        while (true)
        {
            var quadrant = GetQuadrant(currentNode, item.Bounds);
            if (quadrant == -1)
            {
                break;
            }

            currentNode = currentNode.Children[quadrant];
            depth++;
        }

        currentNode.Items.Add(item);
        this.Split(currentNode, depth);
        this.Count++;

        return true;
    }

    private static int GetQuadrant(Node<T> node, Rectangle bounds)
    {
        if (node.Children != null)
        {
            for (int i = 0; i < 4; i++)
            {
                if (bounds.IsInside(node.Children[i].Bounds))
                {
                    return i;
                }
            }
        }

        return -1;
    }

    private void Split(Node<T> node, int depth)
    {
        if (!node.ShouldSplit || depth >= this.MaxDepth)
        {
            return;
        }

        var leftWidth = node.Bounds.MidX - node.Bounds.X1;
        var rightWidth = node.Bounds.Width - leftWidth;
        var topHeight = node.Bounds.MidY - node.Bounds.Y1;
        var bottomHeight = node.Bounds.Height - topHeight;

        if (node.Children == null)
        {
            node.Children = new Node<T>[4];
            node.Children[0] = new Node<T>(node.Bounds.MidX, node.Bounds.Y1, rightWidth, topHeight);
            node.Children[1] = new Node<T>(node.Bounds.X1, node.Bounds.Y1, leftWidth, topHeight);
            node.Children[2] = new Node<T>(node.Bounds.X1, node.Bounds.MidY, leftWidth, bottomHeight);
            node.Children[3] = new Node<T>(node.Bounds.MidX, node.Bounds.MidY, rightWidth, bottomHeight);
        }

        for (int i = node.Items.Count - 1; i >= 0; i--)
        {
            var item = node.Items[i];
            var quadrant = GetQuadrant(node, item.Bounds);
            if (quadrant != -1)
            {
                node.Children[quadrant].Items.Add(item);
                node.Items.Remove(item);
            }
        }

        foreach (var child in node.Children)
        {
            Split(child, depth + 1);
        }
    }

    public List<T> Report(Rectangle bounds)
    {
        var collisionCandidates = new List<T>();

        this.GetCollisionCandidates(this.root, bounds, collisionCandidates);

        return collisionCandidates;
    }

    private void GetCollisionCandidates(Node<T> node, Rectangle bounds, List<T> result)
    {
        var quadrant = GetQuadrant(node, bounds);
        if (quadrant == -1)
        {
            this.ForEachDfs(node, (items, depth, q) =>
            {
                foreach (var item in items)
                {
                    if (item.Bounds.Intersects(bounds))
                    {
                        result.Add(item);
                    }
                }
            });
        }
        else
        {
            this.GetCollisionCandidates(node.Children[quadrant], bounds, result);

            result.AddRange(node.Items.Where(i => i.Bounds.Intersects(bounds)));
        }
    }

    private void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
    {
        if (node == null)
        {
            return;
        }

        if (node.Items.Any())
        {
            action(node.Items, depth, quadrant);
        }

        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                this.ForEachDfs(node.Children[i], action, depth + 1, i);
            }
        }
    }
}