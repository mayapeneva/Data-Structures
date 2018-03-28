using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var matrix = new string[n][];
        var currentRow = 0;
        var currentCol = 0;

        for (int i = 0; i < n; i++)
        {
            matrix[i] = new string[n];
            matrix[i] = Console.ReadLine().ToCharArray().Select(c => c.ToString()).ToArray();
            for (int l = 0; l < n; l++)
            {
                if (matrix[i][l] == "*")
                {
                    currentRow = i;
                    currentCol = l;
                }
            }
        }

        var visitedCells = new Queue<Cell>();
        var startingCell = new Cell(currentRow, currentCol);
        startingCell.Value = 0;
        visitedCells.Enqueue(startingCell);
        while (visitedCells.Count > 0)
        {
            VisitCellsAroundCurrentCell(n, matrix, visitedCells);
        }

        MarkUnreachableCells(n, matrix);

        for (int j = 0; j < n; j++)
        {
            Console.WriteLine(String.Join("", matrix[j]));
        }
    }

    private static void VisitCellsAroundCurrentCell(int n, string[][] matrix, Queue<Cell> visitedCells)
    {
        var currentCell = visitedCells.Dequeue();
        var currentRow = currentCell.Row;
        var currentCol = currentCell.Col;
        var currentValue = currentCell.Value + 1;

        if (currentRow - 1 >= 0 && matrix[currentRow - 1][currentCol] == "0")
        {
            visitedCells.Enqueue(new Cell(currentRow - 1, currentCol, currentValue));
            matrix[currentRow - 1][currentCol] = currentValue.ToString();
        }
        if (currentRow + 1 < n && matrix[currentRow + 1][currentCol] == "0")
        {
            visitedCells.Enqueue(new Cell(currentRow + 1, currentCol, currentValue));
            matrix[currentRow + 1][currentCol] = currentValue.ToString();
        }
        if (currentCol - 1 >= 0 && matrix[currentRow][currentCol - 1] == "0")
        {
            visitedCells.Enqueue(new Cell(currentRow, currentCol - 1, currentValue));
            matrix[currentRow][currentCol - 1] = currentValue.ToString();
        }
        if (currentCol + 1 < n && matrix[currentRow][currentCol + 1] == "0")
        {
            visitedCells.Enqueue(new Cell(currentRow, currentCol + 1, currentValue));
            matrix[currentRow][currentCol + 1] = currentValue.ToString();
        }
    }

    private static void MarkUnreachableCells(int n, string[][] matrix)
    {
        for (int j = 0; j < n; j++)
        {
            for (int k = 0; k < n; k++)
            {
                if (matrix[j][k] == "0")
                {
                    matrix[j][k] = "u";
                }
            }
        }
    }
}