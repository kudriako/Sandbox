namespace MonopolyFoundation
{
    public record Player
    {
        public Player(string name, int cash)
        {
            Name = name;
            Cash = cash;
        }

        public string Name { get; init; } = default!;

        public int Cash { get; set; } = default!;
    }
}
