using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer
{
    private int energy;
    private readonly OrderedBag<Invader> invaders;

    public Computer(int energy)
    {
        this.Energy = energy;
        this.invaders = new OrderedBag<Invader>();
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
        this.invaders.Add(invader);
    }

    public void Skip(int turns)
    {
        var invadersToRemove = new List<Invader>();
        var decreaser = 0;
        foreach (var invader in this.invaders)
        {
            invader.Distance -= turns;
            if (invader.Distance <= 0)
            {
                invadersToRemove.Add(invader);
                decreaser += invader.Damage;
            }
        }

        if (decreaser <= this.Energy)
        {
            this.Energy -= decreaser;
        }
        else
        {
            this.Energy = 0;
        }

        foreach (var invader in invadersToRemove)
        {
            this.invaders.Remove(invader);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        while (this.invaders.Any(a => a.Distance <= radius))
        {
            var invader = this.invaders.OrderBy(a => a).First();
            this.invaders.Remove(invader);
        }
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var invader = this.invaders.OrderBy(a => a).First();
            this.invaders.Remove(invader);
        }
    }

    public IEnumerable<Invader> Invaders()
    {
        return this.invaders;
    }
}