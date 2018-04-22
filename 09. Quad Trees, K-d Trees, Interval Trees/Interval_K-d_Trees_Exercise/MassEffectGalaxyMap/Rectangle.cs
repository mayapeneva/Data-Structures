public class Rectangle
{
    public Rectangle(int x1, int y1, int width, int height)
    {
        this.X1 = x1;
        this.Y1 = y1;
        this.X2 = x1 + width;
        this.Y2 = y1 + height;
    }

    public int Y1 { get; set; }

    public int X1 { get; set; }

    public int Y2 { get; set; }

    public int X2 { get; set; }

    public bool IsInside(Star point)
    {
        return this.X1 <= point.X
            && this.X2 >= point.X
            && this.Y1 <= point.Y &&
               this.Y2 >= point.Y;
    }
}