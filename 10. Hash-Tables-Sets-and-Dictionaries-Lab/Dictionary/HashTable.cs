using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const float LoadFactor = 0.7f;
    private const int DefaultCapacity = 16;

    private LinkedList<KeyValue<TKey, TValue>>[] elements;

    public int Count { get; private set; }

    public int Capacity => this.elements.Length;

    public HashTable(int capacity = DefaultCapacity)
    {
        this.elements = new LinkedList<KeyValue<TKey, TValue>>[capacity];
    }

    public void Add(TKey key, TValue value)
    {
        this.ExpandIfNeeded();

        var index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] == null)
        {
            this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in this.elements[index])
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException();
            }
        }

        this.elements[index].AddLast(new KeyValue<TKey, TValue>(key, value));
        this.Count++;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        this.ExpandIfNeeded();

        var index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] == null)
        {
            this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in this.elements[index])
        {
            if (element.Key.Equals(key))
            {
                element.Value = value;
                return true;
            }
        }

        this.elements[index].AddLast(new KeyValue<TKey, TValue>(key, value));
        this.Count++;
        return false;
    }

    private void ExpandIfNeeded()
    {
        float loadFactor = (float)(this.Count + 1) / this.Capacity;
        if (loadFactor >= LoadFactor)
        {
            var tempArray = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (var kvp in this.elements.Where(e => e != null))
            {
                foreach (var element in kvp)
                {
                    tempArray.Add(element.Key, element.Value);
                }
            }

            this.elements = tempArray.elements;
        }
    }

    public TValue Get(TKey key)
    {
        var kvp = this.Find(key);

        if (kvp == null)
        {
            throw new KeyNotFoundException();
        }

        return kvp.Value;
    }

    public TValue this[TKey key]
    {
        get => this.Get(key);

        set => this.AddOrReplace(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var kvp = this.Find(key);

        if (kvp == null)
        {
            value = default(TValue);
            return false;
        }

        value = kvp.Value;
        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] != null)
        {
            foreach (var element in this.elements[index])
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        return this.Find(key) != null;
    }

    public bool Remove(TKey key)
    {
        var index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] == null)
        {
            return false;
        }

        KeyValue<TKey, TValue> kvpToRemove = null;
        foreach (var element in this.elements[index])
        {
            if (element.Key.Equals(key))
            {
                kvpToRemove = element;
            }
        }

        if (kvpToRemove == null)
        {
            return false;
        }

        this.elements[index].Remove(kvpToRemove);
        this.Count--;
        return true;
    }

    public void Clear()
    {
        this.elements = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            foreach (var kvp in this.elements.Where(e => e != null))
            {
                foreach (var element in kvp)
                {
                    yield return element.Key;
                }
            }
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            foreach (var kvp in this.elements.Where(e => e != null))
            {
                foreach (var element in kvp)
                {
                    yield return element.Value;
                }
            }
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var kvp in this.elements.Where(e => e != null))
        {
            foreach (var element in kvp)
            {
                yield return element;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}