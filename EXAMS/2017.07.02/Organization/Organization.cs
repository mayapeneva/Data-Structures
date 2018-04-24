using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    private readonly Dictionary<string, List<Person>> peopleByName;
    private readonly List<Person> peopleByInsertion;
    private readonly OrderedDictionary<int, List<Person>> peopleByNameLength;

    public Organization()
    {
        this.peopleByName = new Dictionary<string, List<Person>>();
        this.peopleByInsertion = new List<Person>();
        this.peopleByNameLength = new OrderedDictionary<int, List<Person>>();
    }

    public int Count => this.peopleByInsertion.Count;

    public void Add(Person person)
    {
        if (!this.peopleByName.ContainsKey(person.Name))
        {
            this.peopleByName[person.Name] = new List<Person>();
        }

        this.peopleByName[person.Name].Add(person);

        this.peopleByInsertion.Add(person);

        if (!this.peopleByNameLength.ContainsKey(person.Name.Length))
        {
            this.peopleByNameLength[person.Name.Length] = new List<Person>();
        }

        this.peopleByNameLength[person.Name.Length].Add(person);
    }

    public bool Contains(Person person)
    {
        if (this.peopleByName.ContainsKey(person.Name))
        {
            return this.peopleByName[person.Name].Contains(person);
        }

        return false;
    }

    public bool ContainsByName(string name)
    {
        return this.peopleByName.ContainsKey(name);
    }

    public Person GetAtIndex(int index)
    {
        if (index < 0 || index >= this.peopleByInsertion.Count)
        {
            throw new System.IndexOutOfRangeException();
        }

        return this.peopleByInsertion[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (!this.peopleByName.ContainsKey(name))
        {
            return Enumerable.Empty<Person>();
        }

        return this.peopleByName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        if (count <= 0)
        {
            return Enumerable.Empty<Person>();
        }

        if (count >= this.peopleByInsertion.Count)
        {
            count = this.peopleByInsertion.Count;
        }

        return this.peopleByInsertion.Take(count);
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!this.peopleByNameLength.ContainsKey(length))
        {
            throw new ArgumentException();
        }

        return this.peopleByNameLength[length];
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        foreach (var kvp in this.peopleByNameLength.Range(minLength, true, maxLength, true))
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.peopleByInsertion;
    }

    public IEnumerator<Person> GetEnumerator()
    {
        return this.peopleByInsertion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}