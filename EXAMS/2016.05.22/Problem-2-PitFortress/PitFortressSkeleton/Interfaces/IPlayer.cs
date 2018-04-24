namespace Interfaces
{
    using Classes;
    using System;

    public interface IPlayer : IComparable<Player>
    {
        string Name { get; }

        int Radius { get; }

        int Score { get; set; }
    }
}