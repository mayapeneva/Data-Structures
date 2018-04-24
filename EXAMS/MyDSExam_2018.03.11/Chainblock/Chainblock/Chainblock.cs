using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    private readonly Dictionary<int, Transaction> collectionById;
    private readonly Dictionary<TransactionStatus, HashSet<Transaction>> collectionByStatus;
    private readonly OrderedBag<Transaction> collectionByAmount;

    public Chainblock()
    {
        this.collectionById = new Dictionary<int, Transaction>();
        this.collectionByStatus = new Dictionary<TransactionStatus, HashSet<Transaction>>();
        this.collectionByAmount = new OrderedBag<Transaction>();
    }

    public int Count => this.collectionById.Count;

    public void Add(Transaction tx)
    {
        this.collectionById[tx.Id] = tx;

        if (!this.collectionByStatus.ContainsKey(tx.Status))
        {
            this.collectionByStatus[tx.Status] = new HashSet<Transaction>();
        }
        this.collectionByStatus[tx.Status].Add(tx);

        this.collectionByAmount.Add(tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!this.collectionById.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var transaction = this.collectionById[id];
        this.collectionByStatus[transaction.Status].Remove(transaction);
        transaction.Status = newStatus;

        if (!this.collectionByStatus.ContainsKey(newStatus))
        {
            this.collectionByStatus[newStatus] = new HashSet<Transaction>();
        }

        this.collectionByStatus[newStatus].Add(transaction);
    }

    public bool Contains(Transaction tx)
    {
        return this.collectionById.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return this.collectionById.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return this.collectionByAmount.Range(new Transaction(0, default(TransactionStatus), "", "", lo), true,
            new Transaction(0, default(TransactionStatus), "", "", hi), true).Reversed();
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return this.collectionByAmount.OrderByDescending(t => t).ThenBy(t => t.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        if (!this.collectionByStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return this.collectionByStatus[status].OrderByDescending(t => t.Amount).Select(t => t.To);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        if (!this.collectionByStatus.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return this.collectionByStatus[status].OrderByDescending(t => t.Amount).Select(t => t.From);
    }

    public Transaction GetById(int id)
    {
        if (!this.collectionById.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.collectionById[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = this.collectionByAmount.Range(new Transaction(0, default(TransactionStatus), "", "", lo), true,
            new Transaction(0, default(TransactionStatus), "", "", hi), true).Where(t => t.To.Equals(receiver));
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(t => t).ThenBy(t => t.Id);
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.collectionByAmount.Where(t => t.To.Equals(receiver));
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(t => t).ThenBy(t => t.Id);
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.collectionByAmount.RangeFrom(new Transaction(0, default(TransactionStatus), "", "", amount),
            false).Where(t => t.From.Equals(sender));
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(t => t);
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.collectionByAmount.Where(t => t.From.Equals(sender));
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(t => t);
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        var result = this.collectionByAmount.Where(t => t.Status.Equals(status));
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result.OrderByDescending(t => t);
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        return this.collectionByAmount.RangeTo(new Transaction(0, default(TransactionStatus), "", "", amount), true).Where(t => t.Status.Equals(status)).OrderByDescending(t => t);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        return this.collectionById.Values.GetEnumerator();
    }

    public void RemoveTransactionById(int id)
    {
        if (!this.collectionById.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        var transaction = this.collectionById[id];
        this.collectionByStatus[transaction.Status].Remove(transaction);
        if (this.collectionByStatus[transaction.Status].Count == 0)
        {
            this.collectionByStatus.Remove(transaction.Status);
        }

        this.collectionByAmount.Remove(transaction);
        this.collectionById.Remove(id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}