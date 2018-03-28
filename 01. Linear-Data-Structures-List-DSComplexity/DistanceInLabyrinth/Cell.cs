public class Cell
{
    public Cell(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }

    public Cell(int row, int col, int value)
        : this(row, col)
    {
        this.Value = value;
    }

    public int Row { get; }
    public int Col { get; }
    public int Value { get; set; }
}