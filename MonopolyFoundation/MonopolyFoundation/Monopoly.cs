using MonopolyFoundation.Fields;

namespace MonopolyFoundation
{
    public class Monopoly
    {
        public const int StartingCash = 6000;

        public static List<Player> CreatePlayerList(IEnumerable<string> playerNames)
        {
            var list = new List<Player>();
            foreach (var name in playerNames)
            {
                list.Add(new Player(name, StartingCash));
            }
            return list;
        }

        public static List<FieldBase> CreateFieldsList()
        {
            var factory = new FieldFactory();
            var list = new List<FieldBase>();
            list.Add(factory.CreateField("Ford", FieldType.AUTO));
            list.Add(factory.CreateField("MCDonald", FieldType.FOOD));
            list.Add(factory.CreateField("Lamoda", FieldType.CLOTHER));
            list.Add(factory.CreateField("Air Baltic", FieldType.TRAVEL));
            list.Add(factory.CreateField("Nordavia", FieldType.TRAVEL));
            list.Add(factory.CreateField("Prison", FieldType.PRISON));
            list.Add(factory.CreateField("MCDonald", FieldType.FOOD));
            list.Add(factory.CreateField("TESLA", FieldType.AUTO));
            return list;
        }

        private readonly List<Player> _players = new();

        private readonly List<FieldBase> _fields = new();

        public Monopoly(string[] playerNames, int playersNumber)
        {
            if (playerNames == null)
                throw new ArgumentNullException(nameof(playerNames), "Player names does not set.");
            if (playersNumber < 0)
                throw new ArgumentException("Number of players should be no less than zero.");
            if (playersNumber > playerNames.Length)
                throw new ArgumentException("Number of provided player names less that players number.");
            _players = CreatePlayerList(playerNames.Take(playersNumber));
            _fields = CreateFieldsList();
        }

        public IReadOnlyCollection<Player> Players => _players;

        public Player? GetPlayerByIndex(int index) => _players.ElementAtOrDefault(index - 1);

        public IReadOnlyCollection<FieldBase> Fields => _fields;

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

            if (field.HasOwner())
            {
                field.Owner.Cash += field.Income;
            }

            player.Cash -= field.Renta;
 
            var result = field.HasOwner();
            return result;
        }
    }
}
