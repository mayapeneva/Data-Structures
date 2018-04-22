using System;

public class Player : IComparable<Player>
{
    private const int Width = 10;
    private const int Height = 10;

    public Player(string name, int x, int y)
    {
        this.Name = name;
        this.X1 = x;
        this.Y1 = y;
        this.X2 = x + Width;
        this.Y2 = y + Height;
    }

    public string Name { get; set; }
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int Y2 { get; set; }
    public int X2 { get; set; }

    public bool Intersects(Player other)
    {
        return this.X1 <= other.X2 &&
               other.X1 <= this.X2 &&
               this.Y1 <= other.Y2 &&
               other.Y1 <= this.Y2;
    }

    public void Move(int newX1, int newY1)
    {
        this.X1 = newX1;
        this.Y1 = newY1;
        this.X2 = this.X1 + Width;
        this.Y2 = this.Y1 + Height;
    }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;
        if (obj.GetType() != this.GetType()) return false;
        Player that = (Player)obj;
        return this.X1 == that.X1 && this.Y1 == that.Y1;
    }

    public override int GetHashCode()
    {
        int hashX = this.X1.GetHashCode();
        int hashY = this.Y1.GetHashCode();
        return 31 * hashX + hashY;
    }

    public int CompareTo(Player that)
    {
        if (this.Y1 < that.Y1) return -1;
        if (this.Y1 > that.Y1) return +1;
        if (this.X1 < that.X1) return -1;
        if (this.X1 > that.X1) return +1;
        return 0;
    }
}