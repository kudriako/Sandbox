namespace MonopolyFoundation
{
    public abstract record FieldBase
    {
        public FieldBase(string name, FieldType type)
        {
            Name = name;
            FieldType = type;
        }

        public string Name { get; init; } = default!;

        public FieldType FieldType { get; init; } = default!;

        public int Price { get; init; }

        public int Income { get; init; }

        public int Renta { get; init; }

        public abstract bool CanBeBought();

        public Player Owner { get; private set; } = default!;

        public bool HasOwner() => Owner != null;

        public FieldBase SetOwner(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            Owner = player;
            return this;
        }
    }
}
