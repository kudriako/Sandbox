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
            list.Add(new Field("Ford", FieldType.AUTO, 0, false));
            list.Add(new Field("MCDonald", FieldType.FOOD, 0, false));
            list.Add(new Field("Lamoda", FieldType.CLOTHER, 0, false));
            list.Add(new Field("Air Baltic", FieldType.TRAVEL, 0, false));
            list.Add(new Field("Nordavia", FieldType.TRAVEL, 0, false));
            list.Add(new Field("Prison", FieldType.PRISON, 0, false));
            list.Add(new Field("MCDonald", FieldType.FOOD, 0, false));
            list.Add(new Field("TESLA", FieldType.AUTO, 0, false));
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

        public Player GetPlayerInfo(int playerIndex)
        {
            return _players[playerIndex - 1];
        }

        public List<Field> GetFieldsList()
        {
            return _fields;
        }

        public Field GetFieldByName(string fieldName) => (from p in _fields where p.Name == fieldName select p).FirstOrDefault();

        public bool Buy(int playerIndex, Field field)
        {
            if (field.OwnerIndex != 0)
                return false;
            var player = GetPlayerInfo(playerIndex);
            switch (field.FieldType)
            {
                case FieldType.AUTO:
                    player.Cash -= 500;
                    break;

                case FieldType.FOOD:
                    player.Cash -= 250;
                    break;

                case FieldType.TRAVEL:
                    player.Cash -= 700;
                    break;

                case FieldType.CLOTHER:
                    player.Cash -= 100;
                    break;

                default:
                    return false;
            }
            field.OwnerIndex = playerIndex;
            return true;
        }

        public bool Renta(int playerIndex, Field field)
        {
            var player = GetPlayerInfo(playerIndex);
            Player? owner = GetPlayerInfo(field.OwnerIndex);
            switch (field.FieldType)
            {
                case FieldType.AUTO:
                    if (owner == null)
                        return false;
                    player.Cash -= 250;
                    owner.Cash += 250;
                    break;

                case FieldType.FOOD:
                    if (owner == null)
                        return false;
                    player.Cash -= 250;
                    owner.Cash += 250;
                    break;

                case FieldType.TRAVEL:
                    if (owner == null)
                        return false;
                    player.Cash -= 300;
                    owner.Cash += 300;
                    break;

                case FieldType.CLOTHER:
                    if (owner == null)
                        return false;
                    player.Cash -= 100;
                    owner.Cash += 1000;
                    break;

                case FieldType.PRISON:
                    player.Cash -= 1000;
                    break;

                case FieldType.BANK:
                    player.Cash -= 700;
                    break;

                default:
                    return false;
            }
            return true;
        }
    }
}
