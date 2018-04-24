using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var shoppingCenter = new ShoppingCenter();

        var n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine();
            var command = input.Split()[0];
            var tokens = input.Replace(command, "").Trim().Split(';');
            switch (command)
            {
                case "AddProduct":
                    shoppingCenter.AddProduct(tokens[0], decimal.Parse(tokens[1]), tokens[2]);
                    Console.WriteLine("Product added");
                    break;

                case "DeleteProducts":
                    var count = 0;
                    if (tokens.Length == 1)
                    {
                        count = shoppingCenter.DeleteProducts(tokens[0]);
                    }
                    else if (tokens.Length == 2)
                    {
                        count = shoppingCenter.DeleteProducts(tokens[0], tokens[1]);
                    }

                    if (count == 0)
                    {
                        Console.WriteLine("No products found");
                    }
                    else
                    {
                        Console.WriteLine($"{count} products deleted");
                    }
                    break;

                case "FindProductsByName":
                    var result = shoppingCenter.FindProductsByName(tokens[0]);
                    if (!result.Any())
                    {
                        Console.WriteLine("No products found");
                    }
                    else
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                    }
                    break;

                case "FindProductsByProducer":
                    var result2 = shoppingCenter.FindProductsByProducer(tokens[0]);
                    if (!result2.Any())
                    {
                        Console.WriteLine("No products found");
                    }
                    else
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result2));
                    }
                    break;

                case "FindProductsByPriceRange":
                    var result3 = shoppingCenter.FindProductsByPriceRange(decimal.Parse(tokens[0]), decimal.Parse(tokens[1]));
                    if (!result3.Any())
                    {
                        Console.WriteLine("No products found");
                    }
                    else
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result3));
                    }
                    break;
            }
        }
    }
}