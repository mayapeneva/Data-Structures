using System.Collections.Generic;
using Wintellect.PowerCollections;

public class TextEditor : ITextEditor
{
    private Trie<BigList<char>> users;
    private Dictionary<string, Stack<string>> cache;

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

        this.users.Delete(username);
    }

    public void Prepend(string username, string str)
    {
        this.Cache(username);

        if (!this.users.Contains(username))
        {
            return;
        }

        this.users.GetValue(username).AddRangeToFront(str);
    }

    public void Insert(string username, int index, string str)
    {
        this.Cache(username);

        if (!this.users.Contains(username))
        {
            return;
        }

        this.users.GetValue(username).InsertRange(index, str);
    }

    public void Substring(string username, int startIndex, int length)
    {
        this.Cache(username);

        if (!this.users.Contains(username))
        {
            return;
        }

        var newStr = new BigList<char>();
        for (int i = startIndex; i < startIndex + length; i++)
        {
            newStr.Add(this.users.GetValue(username)[i]);
        }

        this.users.Insert(username, newStr);
    }

    public void Delete(string username, int startIndex, int length)
    {
        this.Cache(username);

        if (!this.users.Contains(username))
        {
            return;
        }

        this.users.GetValue(username).RemoveRange(startIndex, length);
    }

    public void Clear(string username)
    {
        this.Cache(username);

        if (!this.users.Contains(username))
        {
            return;
        }

        this.users.GetValue(username).Clear();
    }

    public int Length(string username)
    {
        if (!this.users.Contains(username))
        {
            return default(int);
        }

        return this.users.GetValue(username).Count;
    }

    public string Print(string username)
    {
        if (!this.users.Contains(username))
        {
            return default(string);
        }

        return string.Join("", this.users.GetValue(username));
    }

    public void Undo(string username)
    {
        if (this.cache.Count == 0)
        {
            return;
        }

        this.users.Insert(username, new BigList<char>(this.cache[username].Pop()));
    }

    public IEnumerable<string> Users(string prefix)
    {
        foreach (var user in this.users.GetByPrefix(prefix))
        {
            if (this.users.Contains(user))
            {
                yield return user;
            }
        }
    }

    private void Cache(string username)
    {
        this.cache[username].Push(string.Join("", this.users.GetValue(username)));
    }
}