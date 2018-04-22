using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var clustersNumber = int.Parse(Console.ReadLine());
        var reportsNumber = int.Parse(Console.ReadLine());

        var galaxySize = int.Parse(Console.ReadLine());
        var galaxy = new Rectangle(0, 0, galaxySize, galaxySize);

        var starList = new List<Star>();
        GatherAllStarsCoordinates(clustersNumber, starList, galaxy);

        var stars = new KdTree();
        stars.BuildFromList(starList);

        CheckIfAllStarsAreInRange(reportsNumber, stars);
    }

    private static void CheckIfAllStarsAreInRange(int reportsNumber, KdTree stars)
    {
        for (int m = 0; m < reportsNumber; m++)
        {
            var report = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var x = int.Parse(report[1]);
            var y = int.Parse(report[2]);
            var width = int.Parse(report[3]);
            var height = int.Parse(report[4]);

            var range = new Rectangle(x, y, width, height);
            var starsInRange = stars.EachInOrder(s => range.IsInside(s) ? 1 : 0);

            Console.WriteLine(starsInRange);
        }
    }

    private static void GatherAllStarsCoordinates(int clustersNumber, List<Star> starList, Rectangle galaxy)
    {
        for (int n = 0; n < clustersNumber; n++)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var name = input[0];
            var x = int.Parse(input[1]);
            var y = int.Parse(input[2]);

            var star = new Star(name, x, y);
            if (galaxy.IsInside(star))
            {
                starList.Add(star);
            }
        }
    }
}