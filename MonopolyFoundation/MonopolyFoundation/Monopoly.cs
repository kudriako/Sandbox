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

        public static List<Field> CreateFieldsList()
        {
            var list = new List<Field>();
            list.Add(new Field("Ford", FieldType.AUTO));
            list.Add(new Field("MCDonald", FieldType.FOOD));
            list.Add(new Field("Lamoda", FieldType.CLOTHER));
            list.Add(new Field("Air Baltic", FieldType.TRAVEL));
            list.Add(new Field("Nordavia", FieldType.TRAVEL));
            list.Add(new Field("Prison", FieldType.PRISON));
            list.Add(new Field("MCDonald", FieldType.FOOD));
            list.Add(new Field("TESLA", FieldType.AUTO));
            return list;
        }

        private static readonly Dictionary<FieldType, int> _fieldPrice = new Dictionary<FieldType, int>()
        {
            { FieldType.AUTO, 500 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 700 },
            { FieldType.CLOTHER, 100 },
        };

        private static readonly Dictionary<FieldType, int> _fieldIncome = new Dictionary<FieldType, int>()
        {
            { FieldType.AUTO, 250 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 300 },
            { FieldType.CLOTHER, 1000 },
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

        private readonly List<Player> _players = new List<Player>();

        private readonly List<Field> _fields = new List<Field>();

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

        public List<Player> GetPlayersList()
        {
            return _players;
        }

        public Player? GetPlayerInfo(int playerIndex)
        {
            if (playerIndex < 1 || playerIndex > _players.Count)
                return null;
            return _players[playerIndex - 1];
        }

        public List<Field> GetFieldsList()
        {
            return _fields;
        }

        public Field GetFieldByName(string fieldName) => (from p in _fields where p.Name == fieldName select p).FirstOrDefault();

        public bool Buy(int playerIndex, Field field)
        {
            var player = GetPlayerInfo(playerIndex);
            if (player == null)
                throw new ArgumentException("Invalid player index.");

            if (field.HasOwner())
                return false;

            if (_fieldPrice.TryGetValue(field.FieldType, out int cost))
            {
                field.SetOwner(player);
                player.Cash -= cost;
                return true;
            }
            return false;
        }

        public bool Renta(int playerIndex, Field field)
        {
            var player = GetPlayerInfo(playerIndex);
            if (player == null)
                throw new ArgumentException("Invalid player index.");
            if (field == null)
                throw new ArgumentNullException(nameof(field), "Field does not set.");

            if (_fieldIncome.TryGetValue(field.FieldType, out int income) && field.HasOwner())
            {
                field.Owner.Cash += income;
            }

            if (_fieldRenta.TryGetValue(field.FieldType, out int renta))
            {
                player.Cash -= renta;
            }
 
            var result = field.HasOwner() && income > 0;
            return result;
        }
    }
}
