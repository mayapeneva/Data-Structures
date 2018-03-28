public class ListNode<T>
{
    public ListNode(T value)
    {
        this.Value = value;
    }

    public T Value { get; }
    public ListNode<T> NextNode { get; set; }
    public ListNode<T> PrevNode { get; set; }
}