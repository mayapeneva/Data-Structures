using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

    public static void Main()
    {
        ReadTree();

        var rootNode = nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
        //Console.WriteLine($"Root node: {rootNode.Value}");
        //rootNode.PrintTree();

        //var leafNodes = nodeByValue.Values.Where(x => x.ChildList.Count == 0).Select(x => x.Value).ToArray();
        //Console.WriteLine($"Leaf nodes: {string.Join(" ", leafNodes.OrderBy(x => x))}");

        //var middleNodes = nodeByValue.Values.Where(x => x.Parent != null).Where(x => x.ChildList.Count > 0).Select(x => x.Value).ToArray();
        //Console.WriteLine($"Middle nodes: {string.Join(" ", middleNodes.OrderBy(x => x))}");

        //Tree<int> deepestNode = FindTheDeepestNode();
        //Console.WriteLine($"Deepest node: {deepestNode.Value}");

        //FindAndPrintTheLongestPath();

        var sum = int.Parse(Console.ReadLine());
        //FindAndPrintAllPathsWithTheGivenSum(sum);
        FindAndPrintAllТrееsWithTheGivenSum(sum);
    }

    private static void FindAndPrintAllТrееsWithTheGivenSum(int sum)
    {
        Console.WriteLine($"Subtrees of sum {sum}:");
        foreach (Tree<int> node in nodeByValue.Values)
        {
            var currentTree = new List<int>();
            var currentSum = FindNodeTreeValue(node, currentTree);

            if (currentSum == sum)
            {
                Console.WriteLine(string.Join(" ", currentTree));
            }
        }
    }

    private static int FindNodeTreeValue(Tree<int> node, List<int> currentTree)
    {
        var currentSum = node.Value;
        currentTree.Add(node.Value);
        foreach (var child in node.ChildList)
        {
            currentSum += FindNodeTreeValue(child, currentTree);
        }

        return currentSum;
    }

    private static void FindAndPrintAllPathsWithTheGivenSum(int sum)
    {
        var leafNodes = nodeByValue.Values.Where(x => x.ChildList.Count == 0).ToArray();
        Console.WriteLine($"Paths of sum {sum}:");
        foreach (Tree<int> node in leafNodes)
        {
            var currentSum = 0;
            var currentPath = new List<int>();
            var currentNode = node;
            while (currentNode.Parent != null)
            {
                currentSum += currentNode.Value;
                currentPath.Add(currentNode.Value);
                currentNode = currentNode.Parent;
            }
            currentSum += currentNode.Value;
            currentPath.Add(currentNode.Value);

            if (currentSum == sum)
            {
                currentPath.Reverse();
                Console.WriteLine(string.Join(" ", currentPath));
            }
        }
    }

    private static void FindAndPrintTheLongestPath()
    {
        Tree<int> deepestNode = FindTheDeepestNode();
        var longestPath = new List<int>();
        longestPath.Add(deepestNode.Value);
        var node = deepestNode;
        while (node.Parent != null)
        {
            node = node.Parent;
            longestPath.Add(node.Value);
        }

        longestPath.Reverse();
        Console.WriteLine($"Longest path: {string.Join(" ", longestPath)}");
    }

    private static Tree<int> FindTheDeepestNode()
    {
        var count = 0;
        Tree<int> deepestNode = null;
        foreach (Tree<int> node in nodeByValue.Values.Where(x => x.ChildList.Count == 0).ToArray())
        {
            var currentCount = 0;
            var currentNode = node;
            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
                currentCount++;
            }

            if (currentCount > count)
            {
                count = currentCount;
                deepestNode = node;
            }
        }

        return deepestNode;
    }

    private static void ReadTree()
    {
        var nodesNumber = int.Parse(Console.ReadLine());
        for (int i = 0; i < nodesNumber - 1; i++)
        {
            var input = Console.ReadLine().Split();
            var parent = int.Parse(input[0]);
            var child = int.Parse(input[1]);
            AddEdge(parent, child);
        }
    }

    private static void AddEdge(int parent, int child)
    {
        Tree<int> parentNode = GetTreeNodeByValue(parent);
        Tree<int> childNode = GetTreeNodeByValue(child);

        parentNode.ChildList.Add(childNode);
        childNode.Parent = parentNode;
    }

    private static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }
}