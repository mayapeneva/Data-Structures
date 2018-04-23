using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private readonly Dictionary<string, Person> collectionByEmail;
    private readonly Dictionary<string, SortedDictionary<string, Person>> collectionByDomain;
    private readonly Dictionary<string, SortedDictionary<string, Person>> collectionByNameAndTown;
    private readonly OrderedDictionary<int, SortedDictionary<string, Person>> collectionByAge;
    private readonly Dictionary<string, OrderedDictionary<int, SortedDictionary<string, Person>>> collectionByAgeAndTown;

    public PersonCollection()
    {
        this.collectionByEmail = new Dictionary<string, Person>();
        this.collectionByDomain = new Dictionary<string, SortedDictionary<string, Person>>();
        this.collectionByNameAndTown = new Dictionary<string, SortedDictionary<string, Person>>();
        this.collectionByAge = new OrderedDictionary<int, SortedDictionary<string, Person>>();
        this.collectionByAgeAndTown = new Dictionary<string, OrderedDictionary<int, SortedDictionary<string, Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (!this.collectionByEmail.ContainsKey(email))
        {
            var person = new Person(email, name, age, town);
            this.collectionByEmail.Add(email, person);
            this.AddByDomain(person);
            this.AddByNameAndTown(person);
            this.AddByAge(person);
            this.AddByAgeAndTown(person);
            return true;
        }

        return false;
    }

    private void AddByAgeAndTown(Person person)
    {
        if (!this.collectionByAgeAndTown.ContainsKey(person.Town))
        {
            this.collectionByAgeAndTown[person.Town] = new OrderedDictionary<int, SortedDictionary<string, Person>>();
        }

        if (!this.collectionByAgeAndTown[person.Town].ContainsKey(person.Age))
        {
            this.collectionByAgeAndTown[person.Town][person.Age] = new SortedDictionary<string, Person>();
        }

        this.collectionByAgeAndTown[person.Town][person.Age].Add(person.Email, person);
    }

    private void AddByAge(Person person)
    {
        if (!this.collectionByAge.ContainsKey(person.Age))
        {
            this.collectionByAge[person.Age] = new SortedDictionary<string, Person>();
        }

        this.collectionByAge[person.Age].Add(person.Email, person);
    }

    private void AddByNameAndTown(Person person)
    {
        var nameAndTown = $"{person.Name} {person.Town}";
        if (!this.collectionByNameAndTown.ContainsKey(nameAndTown))
        {
            this.collectionByNameAndTown[nameAndTown] = new SortedDictionary<string, Person>();
        }

        this.collectionByNameAndTown[nameAndTown].Add(person.Email, person);
    }

    private void AddByDomain(Person person)
    {
        var domain = person.Email.Split('@')[1];
        if (!this.collectionByDomain.ContainsKey(domain))
        {
            this.collectionByDomain[domain] = new SortedDictionary<string, Person>();
        }

        this.collectionByDomain[domain].Add(person.Email, person);
    }

    public int Count => this.collectionByEmail.Count;

    public Person FindPerson(string email)
    {
        if (!this.collectionByEmail.ContainsKey(email))
        {
            return null;
        }

        return this.collectionByEmail[email];
    }

    public bool DeletePerson(string email)
    {
        if (!this.collectionByEmail.ContainsKey(email))
        {
            return false;
        }

        var person = this.collectionByEmail[email];
        this.collectionByEmail.Remove(email);

        var domain = email.Split('@')[1];
        this.collectionByDomain[domain].Remove(email);
        this.collectionByNameAndTown.Remove($"{person.Name} {person.Town}");
        this.collectionByAge[person.Age].Remove(email);
        this.collectionByAgeAndTown[person.Town][person.Age].Remove(email);
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (!this.collectionByDomain.ContainsKey(emailDomain))
        {
            return new List<Person>();
        }

        return this.collectionByDomain[emailDomain].Values;
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameAndTown = $"{name} {town}";
        if (!this.collectionByNameAndTown.ContainsKey(nameAndTown))
        {
            return new List<Person>();
        }

        return this.collectionByNameAndTown[nameAndTown].Values;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var result = this.collectionByAge.Range(startAge, true, endAge, true);

        foreach (var resultValue in result.Values)
        {
            foreach (var item in resultValue.Values)
            {
                yield return item;
            }
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        if (!this.collectionByAgeAndTown.ContainsKey(town))
        {
            return new List<Person>();
        }

        var list = this.collectionByAgeAndTown[town].Range(startAge, true, endAge, true);

        var result = new List<Person>();
        foreach (var resultValue in list.Values)
        {
            foreach (var item in resultValue.Values)
            {
                result.Add(item);
            }
        }

        return result;
    }
}