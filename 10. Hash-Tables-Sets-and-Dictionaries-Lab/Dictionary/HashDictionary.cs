using System.Collections;
using System.Collections.Generic;

public class HashDictionary<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private readonly HashTable<TKey, TValue> table;

    public HashDictionary()
    {
        this.table = new HashTable<TKey, TValue>();
    }

    public TValue this[TKey key]
    {
        get => this.table.Get(key);
        set => this.table[key] = value;
    }

    public int Count => this.table.Count;

    public void Add(TKey key, TValue value)
    {
        this.table.AddOrReplace(key, value);
    }

    public void Remove(TKey key)
    {
        this.table.Remove(key);
    }

    public bool Contains(TKey key)
    {
        return this.table.ContainsKey(key);
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var kvp in this.table)
        {
            yield return kvp;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}