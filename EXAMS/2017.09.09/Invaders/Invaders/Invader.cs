using System;

public class Invader : IInvader, IComparable<IInvader>
{
    public Invader(int damage, int distance)
    {
        this.Damage = damage;
        this.Distance = distance;
    }

    public int Damage { get; set; }
    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        var compare = this.Distance.CompareTo(other.Distance);
        if (compare != 0)
        {
            return compare;
        }

        return this.Damage.CompareTo(other.Damage);
    }
}