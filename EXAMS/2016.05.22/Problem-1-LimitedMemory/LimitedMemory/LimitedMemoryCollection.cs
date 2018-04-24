using System.Collections;
using System.Collections.Generic;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<TK, TV> : ILimitedMemoryCollection<TK, TV>
    {
        private readonly LinkedList<Pair<TK, TV>> priorityCollection;
        private readonly Dictionary<TK, LinkedListNode<Pair<TK, TV>>> collection;

        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.priorityCollection = new LinkedList<Pair<TK, TV>>();
            this.collection = new Dictionary<TK, LinkedListNode<Pair<TK, TV>>>();
        }

        public int Capacity { get; }

        public int Count => this.collection.Count;

        public void Set(TK key, TV value)
        {
            if (this.collection.ContainsKey(key))
            {
                var pair = this.collection[key];
                this.priorityCollection.Remove(pair);
                pair.Value.Value = value;
                this.priorityCollection.AddFirst(pair);
            }
            else
            {
                if (this.Count >= this.Capacity)
                {
                    var pairToRemove = this.priorityCollection.Last;
                    this.collection.Remove(pairToRemove.Value.Key);
                    this.priorityCollection.RemoveLast();
                }

                var newPair = new LinkedListNode<Pair<TK, TV>>(new Pair<TK, TV>(key, value));
                this.collection.Add(key, newPair);
                this.priorityCollection.AddFirst(newPair);
            }
        }

        public TV Get(TK key)
        {
            if (!this.collection.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            var pair = this.collection[key];
            this.priorityCollection.Remove(pair);
            this.priorityCollection.AddFirst(pair);
            return pair.Value.Value;
        }

        public IEnumerator<Pair<TK, TV>> GetEnumerator()
        {
            foreach (var kvp in this.priorityCollection)
            {
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}