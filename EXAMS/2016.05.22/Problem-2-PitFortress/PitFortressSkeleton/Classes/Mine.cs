namespace Classes
{
    using Interfaces;
    using System;

    public class Mine : IMine
    {
        private int xCoordinate;
        private int delay;
        private int damage;

        public Mine(int id, int delay, int damage, int xCoordinate, Player player)
        {
            this.Id = id;
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = xCoordinate;
            this.Player = player;
        }

        public int Id { get; }

        public int Delay
        {
            get => this.delay;
            set
            {
                if (value < 0 || value > 10000)
                {
                    throw new ArgumentException();
                }

                this.delay = value;
            }
        }

        public int Damage
        {
            get => this.damage;
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException();
                }

                this.damage = value;
            }
        }

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

        public Player Player { get; }

        public int CompareTo(Mine other)
        {
            var compare = this.Delay.CompareTo(other.Delay);

            if (compare == 0)
            {
                compare = this.Id.CompareTo(other.Id);
            }

            return compare;
        }
    }
}