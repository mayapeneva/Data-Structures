using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        ConstructHeap(arr);
        HeapSort(arr);
    }

    private static void ConstructHeap(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, arr.Length);
        }
    }

    private static void HeapSort(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            var temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            HeapifyDown(arr, 0, i);
        }
    }

    private static void HeapifyDown(T[] arr, int parentIndex, int length)
    {
        if (parentIndex >= length / 2)
        {
            return;
        }

        var childIndex = (parentIndex * 2) + 1;
        if (childIndex + 1 < length)
        {
            var compare = arr[childIndex].CompareTo(arr[childIndex + 1]);
            childIndex = compare > 0 ? childIndex : childIndex + 1;
        }

        var compare2 = arr[parentIndex].CompareTo(arr[childIndex]);
        if (compare2 >= 0)
        {
            return;
        }

        var temp = arr[parentIndex];
        arr[parentIndex] = arr[childIndex];
        arr[childIndex] = temp;

        HeapifyDown(arr, childIndex, length);
    }
}