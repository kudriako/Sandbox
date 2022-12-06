namespace MonopolyFoundation.Fields
{
    public record CommercialField : FieldBase
    {
        public CommercialField(string name, FieldType type) : base(name, type)
        {
        }

        public override bool CanBeBought() => !HasOwner();
    }
}
