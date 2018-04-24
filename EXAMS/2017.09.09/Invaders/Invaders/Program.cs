using System;

public class Program
{
    public static void Main()
    {
        try
        {
            Computer computer = new Computer(100);

            for (int i = 0; i < 10; i++)
            {
                computer.AddInvader(new Invader(1, 1));
            }

            computer.Skip(1);

            Console.WriteLine(computer.Energy);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}