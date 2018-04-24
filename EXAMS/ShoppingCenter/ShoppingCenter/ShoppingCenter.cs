using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class ShoppingCenter
{
    private readonly Dictionary<string, List<Product>> productsByProducer;
    private readonly Dictionary<string, List<Product>> productsByName;
    private readonly OrderedBag<Product> productsByPrice;

    public ShoppingCenter()
    {
        this.productsByProducer = new Dictionary<string, List<Product>>();
        this.productsByName = new Dictionary<string, List<Product>>();
        this.productsByPrice = new OrderedBag<Product>();
    }

    public void AddProduct(string name, decimal price, string producer)
    {
        var product = new Product(name, price, producer);
        if (!this.productsByProducer.ContainsKey(producer))
        {
            this.productsByProducer[producer] = new List<Product>();
        }
        this.productsByProducer[producer].Add(product);

        if (!this.productsByName.ContainsKey(name))
        {
            this.productsByName[name] = new List<Product>();
        }
        this.productsByName[name].Add(product);

        this.productsByPrice.Add(product);
    }

    public int DeleteProducts(string producer)
    {
        if (!this.productsByProducer.ContainsKey(producer))
        {
            return 0;
        }

        var count = this.productsByProducer[producer].Count;

        var productsToRemove = this.productsByProducer[producer];
        foreach (var product in productsToRemove)
        {
            this.productsByName[product.Name].Remove(product);
        }

        this.productsByPrice.RemoveMany(productsToRemove);
        this.productsByProducer.Remove(producer);

        return count;
    }

    public int DeleteProducts(string name, string producer)
    {
        if (!this.productsByProducer.ContainsKey(producer)
            || !this.productsByName.ContainsKey(name))
        {
            return 0;
        }

        var count = this.productsByProducer[producer].Count(p => p.Name.Equals(name));

        var productsToRemove = this.productsByProducer[producer].Where(p => p.Name.Equals(name));
        this.productsByPrice.RemoveMany(productsToRemove);

        this.productsByName[name].RemoveAll(p => p.Producer.Equals(producer));
        this.productsByProducer[producer].RemoveAll(p => p.Name.Equals(name));

        return count;
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
        return this.productsByPrice.Range(new Product("", fromPrice, ""), true, new Product("", toPrice, ""), true).OrderBy(p => p);
    }
}