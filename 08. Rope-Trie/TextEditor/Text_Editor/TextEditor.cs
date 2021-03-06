﻿using System.Collections.Generic;
using Wintellect.PowerCollections;

public class TextEditor : ITextEditor
{
    private readonly Trie<BigList<char>> users;
    private readonly Dictionary<string, Stack<string>> cache;

    public TextEditor()
    {
        this.users = new Trie<BigList<char>>();
        this.cache = new Dictionary<string, Stack<string>>();
    }

    public void Login(string username)
    {
        this.users.Insert(username, new BigList<char>());
        if (!this.cache.ContainsKey(username))
        {
            this.cache[username] = new Stack<string>();
        }
    }

    public void Logout(string username)
    {
        this.Cache(username);
        this.users.GetValue(username).Clear();
    }

    public void Prepend(string username, string str)
    {
        this.Cache(username);

        this.users.GetValue(username).AddRangeToFront(str);
    }

    public void Insert(string username, int index, string str)
    {
        this.Cache(username);

        this.users.GetValue(username).InsertRange(index, str);
    }

    public void Substring(string username, int startIndex, int length)
    {
        this.Cache(username);

        var newStr = new BigList<char>();
        for (int i = startIndex; i < startIndex + length; i++)
        {
            newStr.Add(this.users.GetValue(username)[i]);
        }

        this.users.Insert(username, newStr);

        //this.users.GetValue(username).RemoveRange(0, //startIndex);
        //this.users.GetValue(username).RemoveRange(startIndex + length, this.Length(username));
    }

    public void Delete(string username, int startIndex, int length)
    {
        this.Cache(username);

        this.users.GetValue(username).RemoveRange(startIndex, length);
    }

    public void Clear(string username)
    {
        this.Cache(username);

        this.users.GetValue(username).Clear();
    }

    public int Length(string username)
    {
        return this.users.GetValue(username).Count;
    }

    public string Print(string username)
    {
        return string.Join("", this.users.GetValue(username));
    }

    public void Undo(string username)
    {
        if (this.cache.Count == 0)
        {
            return;
        }

        var cacheString = this.cache[username].Pop();
        this.Cache(username);
        this.users.Insert(username, new BigList<char>(cacheString));
    }

    public IEnumerable<string> Users(string prefix)
    {
        foreach (var user in this.users.GetByPrefix(prefix))
        {
            yield return user;
        }
    }

    private void Cache(string username)
    {
        this.cache[username].Push(string.Join("", this.users.GetValue(username)));
    }
}