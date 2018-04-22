using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Node root;
    private Dictionary<T, Node> nodes;

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.nodes = new Dictionary<T, Node>
        {
            { root, this.root}
        };
    }

    public int Count => this.nodes.Count;

    public void Add(T parent, T child)
    {
        if (!this.nodes.ContainsKey(parent))
        {
            throw new ArgumentException();
        }

        if (this.nodes.ContainsKey(child))
        {
            throw new ArgumentException();
        }

        var parentNode = this.nodes[parent];
        var childNode = new Node(child, parentNode);
        parentNode.Children.Add(childNode);
        this.nodes.Add(child, childNode);
    }

    public void Remove(T element)
    {
        if (!this.nodes.ContainsKey(element))
        {
            throw new ArgumentException();
        }

        var node = this.nodes[element];
        if (node.Parent == null)
        {
            throw new InvalidOperationException();
        }

        node.Parent.Children.Remove(node);
        foreach (var child in node.Children)
        {
            child.Parent = node.Parent;
            node.Parent.Children.Add(child);
        }

        this.nodes.Remove(element);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!this.nodes.ContainsKey(item))
        {
            throw new ArgumentException();
        }

        return this.nodes[item].Children.Select(n => n.Value);
    }

    public T GetParent(T item)
    {
        if (!this.nodes.ContainsKey(item))
        {
            throw new ArgumentException();
        }

        var node = this.nodes[item];
        if (node.Parent != null)
        {
            return node.Parent.Value;
        }

        return default(T);
    }

    public bool Contains(T value)
    {
        return this.nodes.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        //var commonElements = new List<T>();
        //foreach (var item in other.nodesByValue.Keys)
        //{
        //    if (this.nodesByValue.ContainsKey(item))
        //    {
        //        commonElements.Add(item);
        //    }
        //}
        //
        //return commonElements;

        return new HashSet<T>(this.nodes.Keys).Intersect(new HashSet<T>(other.nodes.Keys));
    }

    public IEnumerator<T> GetEnumerator()
    {
        var queue = new Queue<Node>();
        queue.Enqueue(this.root);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            yield return node.Value;

            foreach (var childNode in node.Children)
            {
                queue.Enqueue(childNode);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public class Node
    {
        public Node(T value, Node parent = null)
        {
            this.Value = value;
            this.Children = new List<Node>();
            this.Parent = parent;
        }

        public T Value { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
    }
}