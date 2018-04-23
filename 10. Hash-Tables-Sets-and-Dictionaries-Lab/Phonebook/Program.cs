using System;

public class Program
{
    public static void Main()
    {
        var phoneBook = new Phonebook<string, string>();

        string input;
        while ((input = Console.ReadLine()) != "search")
        {
            var tokens = input.Split('-');
            var name = tokens[0];
            var phoneNumber = tokens[1];

            phoneBook.Add(name, phoneNumber);
        }

        while ((input = Console.ReadLine()) != "end")
        {
            Console.WriteLine(phoneBook.Contains(input)
                ? $"{input} -> {phoneBook[input]}"
                : $"Contact {input} does not exist.");
        }
    }
}