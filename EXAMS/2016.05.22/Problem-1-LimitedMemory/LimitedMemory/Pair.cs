namespace LimitedMemory
{
    public class Pair<TK, TV>
    {
        public Pair(TK key, TV value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TK Key { get; set; }

        public TV Value { get; set; }
    }
}