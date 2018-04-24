namespace Classes
{
    using Interfaces;
    using System;

    public class Minion : IMinion
    {
        private int xCoordinate;

        public Minion(int id, int xCoordinate, int health = 100)
        {
            this.Id = id;
            this.XCoordinate = xCoordinate;
            this.Health = health;
        }

        public int Id { get; }

        public int XCoordinate
        {
            get => this.xCoordinate;
            private set
            {
                if (value < 0 || value > 1000000)
                {
                    throw new ArgumentException();
                }

                this.xCoordinate = value;
            }
        }

        public int Health { get; set; }

        public int CompareTo(Minion other)
        {
            var compare = this.XCoordinate.CompareTo(other.XCoordinate);

            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }

            return compare;
        }
    }
}