using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var shoppingBag = new ShoppingBag();

        var n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine();
            var command = input.Split()[0];
            var args = input.Replace($"{command}", "").Trim();
            var tokens = args.Split(';');

            switch (command)
            {
                case "AddProduct":
                    var product = new Product(tokens[0], decimal.Parse(tokens[1]), tokens[2]);
                    shoppingBag.AddProduct(product);
                    Console.WriteLine("Product added");
                    break;

                case "DeleteProducts":
                    Console.WriteLine(!args.Contains(";")
                        ? shoppingBag.DeleteProducts(args)
                        : shoppingBag.DeleteProducts(tokens[0], tokens[1]));
                    break;

                case "FindProductsByName":
                    var result = shoppingBag.FindProductsByName(args);
                    if (result.Any())
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                        break;
                    }

                    Console.WriteLine("No products found");
                    break;

                case "FindProductsByProducer":
                    result = shoppingBag.FindProductsByProducer(args);
                    if (result.Any())
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                        break;
                    }

                    Console.WriteLine("No products found");
                    break;

                case "FindProductsByPriceRange":
                    var prices = args.Split(';').Select(decimal.Parse).ToArray();
                    result = shoppingBag.FindProductsByPriceRange(prices[0], prices[1]);
                    if (result.Any())
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result));
                        break;
                    }

                    Console.WriteLine("No products found");
                    break;
            }
        }
    }
}