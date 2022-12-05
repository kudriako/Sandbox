using System.Linq.Expressions;

namespace MonopolyFoundation.Fields
{
    public abstract record FieldBase
    {
        private static readonly Dictionary<FieldType, int> _fieldPrice = new Dictionary<FieldType, int>()
        {
            { FieldType.AUTO, 500 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 700 },
            { FieldType.CLOTHER, 100 },
            { FieldType.PRISON, 0 },
            { FieldType.BANK, 0 },
        };

        private static readonly Dictionary<FieldType, int> _fieldIncome = new Dictionary<FieldType, int>()
        {
            { FieldType.AUTO, 250 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 300 },
            { FieldType.CLOTHER, 1000 },
            { FieldType.PRISON, 0 },
            { FieldType.BANK, 0 },
        };

        private static readonly Dictionary<FieldType, int> _fieldRenta = new Dictionary<FieldType, int>()
        {
            { FieldType.AUTO, 250 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 300 },
            { FieldType.CLOTHER, 100 },
            { FieldType.PRISON, 1000 },
            { FieldType.BANK, 700 },
        };

        public FieldBase(string name, FieldType type)
        {
            Name = name;
            FieldType = type;

            Price = _fieldPrice[type];
            Income = _fieldIncome[type];
            Renta = _fieldRenta[type];
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
