using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class ShoppingBag
{
    private readonly Dictionary<string, Bag<Product>> productsByProducer;
    private readonly Dictionary<string, Bag<Product>> productsByName;
    private readonly Dictionary<string, Bag<Product>> productsByNameAndProducer;
    private readonly OrderedBag<Product> productsbyPrice;

    public ShoppingBag()
    {
        this.productsByProducer = new Dictionary<string, Bag<Product>>();
        this.productsByName = new Dictionary<string, Bag<Product>>();
        this.productsByNameAndProducer = new Dictionary<string, Bag<Product>>();
        this.productsbyPrice = new OrderedBag<Product>((a, b) => a.Price.CompareTo(b.Price));
    }

    public void AddProduct(Product product)
    {
        if (!this.productsByProducer.ContainsKey(product.Producer))
        {
            this.productsByProducer[product.Producer] = new Bag<Product>();
        }
        this.productsByProducer[product.Producer].Add(product);

        if (!this.productsByName.ContainsKey(product.Name))
        {
            this.productsByName[product.Name] = new Bag<Product>();
        }
        this.productsByName[product.Name].Add(product);

        var nameAndProducer = $"{product.Name}{product.Producer}";
        if (!this.productsByNameAndProducer.ContainsKey(nameAndProducer))
        {
            this.productsByNameAndProducer[nameAndProducer] = new Bag<Product>();
        }
        this.productsByNameAndProducer[nameAndProducer].Add(product);

        this.productsbyPrice.Add(product);
    }

    public string DeleteProducts(string producer)
    {
        if (!this.productsByProducer.ContainsKey(producer))
        {
            return "No products found";
        }

        var productsToRemove = this.productsByProducer[producer];
        var count = productsToRemove.Count;

        foreach (var product in productsToRemove)
        {
            this.productsByName[product.Name].Remove(product);
            this.productsByNameAndProducer[$"{product.Name}{product.Producer}"].Remove(product);
            this.productsbyPrice.Remove(product);
        }

        this.productsByProducer.Remove(producer);

        return $"{count} products deleted";
    }

    public string DeleteProducts(string name, string producer)
    {
        if (!this.productsByProducer.ContainsKey(producer))
        {
            return "No products found";
        }

        var nameAndProducer = $"{name}{producer}";
        var productsToRemove = this.productsByNameAndProducer[nameAndProducer];
        var count = productsToRemove.Count;

        foreach (var product in productsToRemove)
        {
            this.productsByName[product.Name].Remove(product);
            this.productsByProducer[product.Producer].Remove(product);
            this.productsbyPrice.Remove(product);
        }

        this.productsByNameAndProducer.Remove(nameAndProducer);

        return $"{count} products deleted";
    }

    public IEnumerable<Product> FindProductsByName(string name)
    {
        if (!this.productsByName.ContainsKey(name))
        {
            return Enumerable.Empty<Product>();
        }

        return this.productsByName[name].OrderBy(p => p);
    }

    public IEnumerable<Product> FindProductsByProducer(string producer)
    {
        if (!this.productsByProducer.ContainsKey(producer))
        {
            return Enumerable.Empty<Product>();
        }

        return this.productsByProducer[producer].OrderBy(p => p);
    }

    public IEnumerable<Product> FindProductsByPriceRange(decimal fromPrice, decimal toPrice)
    {
        return this.productsbyPrice.Range(new Product("", fromPrice, ""), true, new Product("", toPrice, ""), true).OrderBy(p => p);
    }
}