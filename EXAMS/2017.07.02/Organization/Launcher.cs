using System;

public class Launcher
{
    public static void Main()
    {
        try
        {
            IOrganization org = new Organization();

            org.Add(new Person("Ivan", 350));
            org.Add(new Person("Pesho", 1200));
            org.Add(new Person("Mitko", 20));
            org.Add(new Person("Maria", 0));
            org.Add(new Person("Stamat", 1500));
            org.Add(new Person("Alex", 850));
            org.Add(new Person("Rosi", 3000));

            var search = org.GetWithNameSize(-1);

            foreach (var person in search)
            {
                Console.WriteLine(person.Name);
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}