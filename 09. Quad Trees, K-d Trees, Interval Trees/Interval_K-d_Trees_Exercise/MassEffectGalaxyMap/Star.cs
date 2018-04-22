public class Star
{
    public Star(string name, double x, double y)
    {
        this.Name = name;
        this.X = x;
        this.Y = y;
    }

    public string Name { get; set; }
    public double X { get; set; }
    public double Y { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;
        if (obj.GetType() != this.GetType()) return false;
        Star that = (Star)obj;
        return this.X == that.X && this.Y == that.Y;
    }

    public override int GetHashCode()
    {
        int hashX = this.X.GetHashCode();
        int hashY = this.Y.GetHashCode();
        return 31 * hashX + hashY;
    }

    public int CompareTo(Star that)
    {
        if (this.Y < that.Y) return -1;
        if (this.Y > that.Y) return +1;
        if (this.X < that.X) return -1;
        if (this.X > that.X) return +1;
        return 0;
    }
}