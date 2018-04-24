namespace Interfaces
{
    using Classes;
    using System;

    public interface IMinion : IComparable<Minion>
    {
        int Id { get; }

        int XCoordinate { get; }

        int Health { get; set; }
    }
}