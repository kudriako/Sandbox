namespace MonopolyFoundation.Fields
{
    public record MunicipalField : FieldBase
    {
        public MunicipalField(string name, FieldType type) : base(name, type)
        {
        }

        public override bool CanBeBought() => false;
    }
}
