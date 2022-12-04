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

            if (field.CanBeBought())
            {
                field.SetOwner(player);
                player.Cash -= field.Price;
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
