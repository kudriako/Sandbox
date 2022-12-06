using MonopolyFoundation.Fields;

namespace MonopolyFoundation
{
    public class FieldFactory
    {
        private static readonly Dictionary<FieldType, Func<string, FieldBase>> _fieldTypes = new Dictionary<FieldType, Func<string, FieldBase>>()
        {
            { FieldType.AUTO, (name) => new CommercialField(name, FieldType.AUTO) { Price = 500, Income = 250, Renta = 250 } },
            { FieldType.FOOD, (name) => new CommercialField(name, FieldType.FOOD) { Price = 250, Income = 250, Renta = 250 } },
            { FieldType.TRAVEL, (name) => new CommercialField(name, FieldType.TRAVEL) { Price = 700, Income = 300, Renta = 300 } },
            { FieldType.CLOTHER, (name) => new CommercialField(name, FieldType.CLOTHER) { Price = 100, Income = 1000, Renta = 100 } },
            { FieldType.PRISON, (name) => new MunicipalField(name, FieldType.PRISON) { Renta = 1000 } },
            { FieldType.BANK, (name) => new CommercialField(name, FieldType.BANK) { Renta = 700 } },
        };

        public FieldBase CreateField(string name, FieldType type)
        {
            return _fieldTypes[type](name);
        }
    }
}
