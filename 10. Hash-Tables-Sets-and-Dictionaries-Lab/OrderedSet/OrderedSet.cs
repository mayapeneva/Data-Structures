using System;
using System.Collections;
using System.Collections.Generic;

public class OrderedSet<T> : IEnumerable<T> where T : IComparable
{
    private readonly BinarySearchTree<T> tree;

    public OrderedSet()
    {
        this.tree = new BinarySearchTree<T>();
    }

    public int Count => this.tree.Count();

    public void Add(T element)
    {
        this.tree.Insert(element);
    }

    public bool Contains(T element)
    {
        return this.tree.Contains(element);
    }

    public void Remove(T element)
    {
        this.tree.Delete(element);
    }

    public IEnumerator<T> GetEnumerator()
    {
        var result = new List<T>();
        this.tree.EachInOrder(result.Add);

        foreach (var item in result)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}