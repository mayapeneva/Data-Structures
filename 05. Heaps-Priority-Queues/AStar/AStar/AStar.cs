using System;
using System.Collections.Generic;

public class AStar
{
    private char[,] map;
    private PriorityQueue<Node> pQ;
    private Dictionary<Node, Node> parents;
    private Dictionary<Node, int> gCost;

    public AStar(char[,] map)
    {
        this.map = map;
        this.pQ = new PriorityQueue<Node>();
        this.parents = new Dictionary<Node, Node>();
        this.gCost = new Dictionary<Node, int>();
    }

    public static int GetH(Node current, Node goal)
    {
        var row = Math.Abs(current.Row - goal.Row);
        var col = Math.Abs(current.Col - goal.Col);

        return row + col;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        this.gCost.Add(start, 0);
        this.parents.Add(start, null);
        this.pQ.Enqueue(start);

        while (this.pQ.Count > 0)
        {
            var current = this.pQ.Dequeue();
            if (current.Equals(goal))
            {
                break;
            }

            List<Node> neighbors = this.GetNeighborNodes(current);

            var newGCost = this.gCost[current] + 1;
            foreach (var neighbor in neighbors)
            {
                if (!this.gCost.ContainsKey(neighbor) || newGCost < this.gCost[neighbor])
                {
                    neighbor.F = newGCost + GetH(neighbor, goal);
                    this.parents[neighbor] = current;
                    this.gCost[neighbor] = newGCost;
                    this.pQ.Enqueue(neighbor);
                }
            }
        }

        return this.ReconstructPath(this.parents, start, goal);
    }

    private IEnumerable<Node> ReconstructPath(Dictionary<Node, Node> dictionary, Node start, Node goal)
    {
        if (!parents.ContainsKey(goal))
        {
            return new List<Node>
            {
                start
            };
        }

        var current = parents[goal];
        var path = new Stack<Node>();
        path.Push(goal);
        while (!current.Equals(start))
        {
            path.Push(current);
            current = parents[current];
        }

        path.Push(start);
        return path;
    }

    private List<Node> GetNeighborNodes(Node current)
    {
        var neighbors = new List<Node>();

        var row = current.Row;
        var col = current.Col;
        if (this.IsInBounds(row - 1, col) && this.IsNotWall(row - 1, col))
        {
            neighbors.Add(new Node(row - 1, col));
        }

        if (this.IsInBounds(row + 1, col) && this.IsNotWall(row + 1, col))
        {
            neighbors.Add(new Node(row + 1, col));
        }

        if (this.IsInBounds(row, col - 1) && this.IsNotWall(row, col - 1))
        {
            neighbors.Add(new Node(row, col - 1));
        }

        if (this.IsInBounds(row, col + 1) && this.IsNotWall(row, col + 1))
        {
            neighbors.Add(new Node(row, col + 1));
        }

        return neighbors;
    }

    private bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < this.map.GetLength(0) && col >= 0 && col < this.map.GetLength(1);
    }

    private bool IsNotWall(int row, int col)
    {
        return this.map[row, col] != 'W';
    }
}