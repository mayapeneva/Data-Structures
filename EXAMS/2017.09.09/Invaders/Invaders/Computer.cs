using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    private int energy;
    private readonly Dictionary<int, List<LinkedListNode<Invader>>> invadersByDistance;
    private readonly LinkedList<Invader> invadersByInsertion;

    public Computer(int energy)
    {
        this.Energy = energy;
        this.invadersByDistance = new Dictionary<int, List<LinkedListNode<Invader>>>();
        this.invadersByInsertion = new LinkedList<Invader>();
    }

    public int Energy
    {
        get
        {
            if (this.energy <= 0)
            {
                return 0;
            }

            return this.energy;
        }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException();
            }

            this.energy = value;
        }
    }

    public void AddInvader(Invader invader)
    {
        if (!this.invadersByDistance.ContainsKey(invader.Distance))
        {
            this.invadersByDistance[invader.Distance] = new List<LinkedListNode<Invader>>();
        }

        this.invadersByInsertion.AddLast(invader);
        this.invadersByDistance[invader.Distance].Add(new LinkedListNode<Invader>(invader));
    }

    public void Skip(int turns)
    {
        var decreaser = 0;
        var temp = new List<Invader>();
        foreach (var inv in this.invadersByInsertion)
        {
            inv.Distance -= turns;
            if (inv.Distance <= 0)
            {
                decreaser += inv.Damage;
                temp.Add(inv);
            }
        }

        if (decreaser < this.Energy)
        {
            this.Energy -= decreaser;
        }
        else
        {
            this.Energy = 0;
        }

        foreach (var invader in temp)
        {
            this.invadersByDistance.Remove(invader.Distance);
            this.invadersByInsertion.Remove(invader);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        var temp = new List<Invader>();
        foreach (var inv in this.invadersByDistance.SelectMany(i => i.Value))
        {
            if (inv.Value.Distance <= radius)
            {
                temp.Add(inv.Value);
            }
        }

        foreach (var invader in temp)
        {
            this.invadersByDistance.Remove(invader.Distance);
            this.invadersByInsertion.Remove(invader);
        }
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        var counter = 0;
        foreach (var invader in this.invadersByDistance.SelectMany(i => i.Value).OrderBy(i => i))
        {
            this.invadersByInsertion.Remove(invader);
            counter++;

            if (counter == count)
            {
                break;
            }
        }

        var temp = this.invadersByDistance.SelectMany(i => i.Value).OrderBy(i => i).Skip(counter).ToList();
        this.invadersByDistance.Clear();

        foreach (var invader in temp)
        {
            if (!this.invadersByDistance.ContainsKey(invader.Value.Distance))
            {
                this.invadersByDistance[invader.Value.Distance] = new List<LinkedListNode<Invader>>();
            }

            this.invadersByDistance[invader.Value.Distance].Add(invader);
        }
    }

    public IEnumerable<Invader> Invaders()
    {
        foreach (var invader in this.invadersByInsertion)
        {
            yield return invader;
        }
    }
}