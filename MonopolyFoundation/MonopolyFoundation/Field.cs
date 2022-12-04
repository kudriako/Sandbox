using System.Linq.Expressions;

namespace MonopolyFoundation
{
    public record Field
    {
        public Field(string name, FieldType type)
        {
            Name = name;
            FieldType = type;
        }

        public string Name { get; init; } = default!;

        public FieldType FieldType { get; init; } = default!;

        public Player Owner { get; private set; } = default!;

        public bool HasOwner() => Owner != null;

        public Field SetOwner(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            Owner = player;
            return this;
        }
    }
}
