﻿using System;

public class Program
{
    public static void Main(string[] args)
    {
        FirstLastList<int> list = new FirstLastList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Console.WriteLine();
    }
}