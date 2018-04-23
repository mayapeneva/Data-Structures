using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var players = new List<Player>();

        GatherPlayersInfo(players);

        var turns = 1;
        string input;
        while ((input = Console.ReadLine()) != "end")
        {
            var command = input.Split();
            if (command[0].Equals("move"))
            {
                var name = command[1];
                var newX1 = int.Parse(command[2]);
                var newY1 = int.Parse(command[3]);
                var player = players.FirstOrDefault(p => p.Name.Equals(name));
                player?.Move(newX1, newY1);
            }

            players = players.OrderBy(p => p.X1).ToList();
            for (int i = 0; i < players.Count - 1; i++)
            {
                var currentPlayer = players[i];
                for (int j = i + 1; j < players.Count; j++)
                {
                    var collisionCandidate = players[j];
                    if (currentPlayer.X2 < collisionCandidate.X1)
                    {
                        break;
                    }

                    if (currentPlayer.Intersects(collisionCandidate))
                    {
                        Console.WriteLine($"({turns}) {currentPlayer.Name} collides with {collisionCandidate.Name}");
                    }
                }
            }

            turns++;
        }
    }

    private static void GatherPlayersInfo(List<Player> players)
    {
        string input;
        while ((input = Console.ReadLine()) != "start")
        {
            var tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var name = tokens[1];
            var x = int.Parse(tokens[2]);
            var y = int.Parse(tokens[3]);

            players.Add(new Player(name, x, y));
        }
    }
}