using Classes;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PitFortressCollection : IPitFortress
{
    private Dictionary<string, Player> players;
    private SortedSet<Player> playersOrdered;
    private OrderedDictionary<int, SortedSet<Minion>> minions;
    private SortedSet<Mine> mines;

    private int minionId;
    private int mineId;

    public PitFortressCollection()
    {
        this.players = new Dictionary<string, Player>();
        this.playersOrdered = new SortedSet<Player>();
        this.minions = new OrderedDictionary<int, SortedSet<Minion>>();
        this.mines = new SortedSet<Mine>();
        this.minionId = 1;
        this.mineId = 1;
    }

    public int PlayersCount => this.players.Count;

    public int MinionsCount => this.minions.Sum(a => a.Value.Count);

    public int MinesCount => this.mines.Count;

    public void AddPlayer(string name, int mineRadius)
    {
        if (this.players.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        if (mineRadius < 0)
        {
            throw new ArgumentException();
        }

        var player = new Player(name, mineRadius);
        this.players.Add(name, player);
        this.playersOrdered.Add(player);
    }

    public void AddMinion(int xCoordinate)
    {
        if (!this.minions.ContainsKey(xCoordinate))
        {
            this.minions[xCoordinate] = new SortedSet<Minion>();
        }

        this.minions[xCoordinate].Add(new Minion(this.minionId++, xCoordinate));
    }

    public void SetMine(string playerName, int xCoordinate, int delay, int damage)
    {
        if (!this.players.ContainsKey(playerName))
        {
            throw new ArgumentException();
        }

        this.mines.Add(new Mine(this.mineId++, delay, damage, xCoordinate, this.players[playerName]));
    }

    public IEnumerable<Minion> ReportMinions()
    {
        foreach (var kvp in this.minions.Values)
        {
            foreach (var minion in kvp)
            {
                yield return minion;
            }
        }
    }

    public IEnumerable<Player> Top3PlayersByScore()
    {
        if (this.players.Count < 3)
        {
            throw new ArgumentException();
        }

        return this.playersOrdered.Reverse().Take(3);
    }

    public IEnumerable<Player> Min3PlayersByScore()
    {
        if (this.players.Count < 3)
        {
            throw new ArgumentException();
        }

        return this.playersOrdered.Take(3);
    }

    public IEnumerable<Mine> GetMines()
    {
        return this.mines;
    }

    public void PlayTurn()
    {
        var minesToExplode = new List<Mine>();
        foreach (var mine in this.mines)
        {
            mine.Delay--;

            if (mine.Delay <= 0)
            {
                minesToExplode.Add(mine);
            }
        }

        foreach (var mineToExplode in minesToExplode)
        {
            var from = mineToExplode.XCoordinate - mineToExplode.Player.Radius;
            var to = mineToExplode.XCoordinate + mineToExplode.Player.Radius;
            var minionsInRange = this.minions.Range(from, true, to, true).SelectMany(x => x.Value);

            foreach (var minion in minionsInRange)
            {
                minion.Health -= mineToExplode.Damage;

                if (minion.Health <= 0)
                {
                    mineToExplode.Player.Score++;
                    this.minions[minion.XCoordinate].Remove(minion);
                }
            }

            this.mines.Remove(mineToExplode);
        }
    }
}