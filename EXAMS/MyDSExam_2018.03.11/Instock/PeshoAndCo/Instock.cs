using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Instock : IProductStock
{
    //by Insertion to be OrderedSet
    private readonly List<Product> productsByInsertion;

    private readonly Dictionary<string, Product> productsByLabel;
    private readonly HashSet<Product> productsByQuantity;
    private readonly OrderedDictionary<double, HashSet<Product>> productsByPrice;

    public Instock()
    {
        this.productsByInsertion = new List<Product>();
        this.productsByLabel = new Dictionary<string, Product>();
        this.productsByQuantity = new HashSet<Product>();
        this.productsByPrice = new OrderedDictionary<double, HashSet<Product>>((a, b) => a.CompareTo(b));
    }

    public int Count => this.productsByInsertion.Count;

    public void Add(Product product)
    {
        this.productsByInsertion.Add(product);

        this.productsByLabel[product.Label] = product;

        this.productsByQuantity.Add(product);

        if (!this.productsByPrice.ContainsKey(product.Price))
        {
            this.productsByPrice[product.Price] = new HashSet<Product>();
        }
        this.productsByPrice[product.Price].Add(product);
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.productsByLabel.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var productItem = this.productsByLabel[product];
        this.productsByQuantity.Remove(productItem);
        productItem.Quantity = quantity;
        this.productsByQuantity.Add(productItem);
    }

    public bool Contains(Product product)
    {
        return this.productsByLabel.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index < 0 || index > this.productsByInsertion.Count - 1)
        {
            throw new IndexOutOfRangeException();
        }

        return this.productsByInsertion[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        return this.productsByPrice.Range(price, true, price, true).SelectMany(p => p.Value);
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        return this.productsByQuantity.Where(p => p.Quantity.Equals(quantity));
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        return this.productsByPrice.Range(lo, false, hi, true).SelectMany(p => p.Value).OrderByDescending(p => p);
    }

    public Product FindByLabel(string label)
    {
        if (!this.productsByLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return this.productsByLabel[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count > this.productsByLabel.Count)
        {
            throw new ArgumentException();
        }

        return this.productsByLabel.Select(p => p.Value).OrderBy(p => p.Label).Take(count);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (count > this.productsByLabel.Count)
        {
            throw new ArgumentException();
        }

        return this.productsByPrice.SelectMany(p => p.Value).OrderByDescending(p => p).Take(count);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        return this.productsByInsertion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}