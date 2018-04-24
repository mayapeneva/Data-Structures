namespace Classes
{
    using Interfaces;
    using System;

    public class Player : IPlayer
    {
        private int radius;

        public Player(string name, int radius)
        {
            this.Name = name;
            this.Radius = radius;
        }

        public string Name { get; }

        public int Radius
        {
            get => this.radius;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                this.radius = value;
            }
        }

        public int Score { get; set; }

        public int CompareTo(Player other)
        {
            var compare = this.Score.CompareTo(other.Score);

            if (compare == 0)
            {
                compare = String.Compare(this.Name, other.Name, StringComparison.Ordinal);
            }
            return compare;
        }
    }
}