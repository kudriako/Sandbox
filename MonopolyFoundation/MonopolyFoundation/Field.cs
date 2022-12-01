namespace MonopolyFoundation
{
    public record Field
    {
        public Field(string name, FieldType type, int ownerIndex, bool field4)
        {
            Name = name;
            FieldType = type;
            OwnerIndex = ownerIndex;
            Field4 = field4;
        }

        public string Name { get; init; } = default!;

        public FieldType FieldType { get; init; } = default!;

        public int OwnerIndex { get; set; } = default!;

        public bool Field4 { get; set; } = default!;
    }
}
