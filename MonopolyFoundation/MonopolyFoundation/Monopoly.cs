namespace MonopolyFoundation
{
    public class Monopoly
    {
        private readonly List<Player> _players = new();

        private readonly List<FieldBase> _fields = new();

        public IReadOnlyCollection<Player> Players => _players;

        public void AddPlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));
            _players.Add(player);
        }

        public Player? GetPlayerByIndex(int index) => _players.ElementAtOrDefault(index - 1);

        public IReadOnlyCollection<FieldBase> Fields => _fields;

        public void AddField(FieldBase field)
        {
            if (field == null)
                throw new ArgumentNullException(nameof(field));
            _fields.Add(field);
        }

        public FieldBase? GetFieldByName(string name) => _fields.FirstOrDefault(f => f.Name == name);

        public bool Buy(int playerIndex, FieldBase field)
        {
            var player = GetPlayerByIndex(playerIndex);
            if (player == null)
                throw new ArgumentException("Invalid player index.");
            if (field.CanBeBought())
            {
                field.SetOwner(player);
                player.Cash -= field.Price;
                return true;
            }
            return false;
        }

        public bool Renta(int playerIndex, FieldBase field)
        {
            var player = GetPlayerByIndex(playerIndex);
            if (player == null)
                throw new ArgumentException("Invalid player index.");
            if (field == null)
                throw new ArgumentNullException(nameof(field), "Field does not set.");

            player.Cash -= field.Charge;

            if (field.HasOwner())
            {
                field.Owner.Cash += field.Income;
                return true;
            }
            return false;
        }
    }
}
