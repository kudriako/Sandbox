namespace MonopolyFoundation
{
    public record Field
    {
        public Field(string name, FieldType type, int ownerIndex)
        {
            Name = name;
            FieldType = type;
            OwnerIndex = ownerIndex;
        }

        public string Name { get; init; } = default!;

        public FieldType FieldType { get; init; } = default!;

        public int OwnerIndex { get; set; } = default!;
    }
}
