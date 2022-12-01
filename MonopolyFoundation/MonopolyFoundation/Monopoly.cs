using System.Collections.Generic;
using System.Linq;

namespace MonopolyFoundation
{
    public class Monopoly
    {
        private readonly List<Player> _players = new List<Player>();

        public List<(string, FieldType, int, bool)> fields = new List<(string, FieldType, int, bool)>();

        public Monopoly(string[] p, int v)
        {
            for (int i = 0; i < v; i++)
            {
                _players.Add(new Player(p[i], 6000));
            }
            fields.Add(("Ford", FieldType.AUTO, 0, false));
            fields.Add(("MCDonald", FieldType.FOOD, 0, false));
            fields.Add(("Lamoda", FieldType.CLOTHER, 0, false));
            fields.Add(("Air Baltic", FieldType.TRAVEL, 0, false));
            fields.Add(("Nordavia", FieldType.TRAVEL, 0, false));
            fields.Add(("Prison", FieldType.PRISON, 0, false));
            fields.Add(("MCDonald", FieldType.FOOD, 0, false));
            fields.Add(("TESLA", FieldType.AUTO, 0, false));
        }

        public List<Player> GetPlayersList()
        {
            return _players;
        }

        

        public List<(string, FieldType, int, bool)> GetFieldsList()
        {
            return fields;
        }

        public (string, FieldType, int, bool) GetFieldByName(string v)
        {
            return (from p in fields where p.Item1 == v select p).FirstOrDefault();
        }

        public bool Buy(int playerIndex, (string, FieldType, int, bool) k)
        {
            var player = GetPlayerInfo(playerIndex);
            int cash = 0;
            switch (k.Item2)
            {
                case FieldType.AUTO:
                    if (k.Item3 != 0)
                        return false;
                    player.Cash -= 500;
                    break;

                case FieldType.FOOD:
                    if (k.Item3 != 0)
                        return false;
                    player.Cash -= 250;
                    break;

                case FieldType.TRAVEL:
                    if (k.Item3 != 0)
                        return false;
                    player.Cash -= 700;
                    break;

                case FieldType.CLOTHER:
                    if (k.Item3 != 0)
                        return false;
                    player.Cash -= 100;
                    break;

                default:
                    return false;
            }
            int i = _players.Select((item, index) => new { name = item.Name, index = index })
                .Where(n => n.name == player.Name)
                .Select(p => p.index).FirstOrDefault();
            fields[i] = (k.Item1, k.Item2, playerIndex, k.Item4);
            return true;
        }

        public Player GetPlayerInfo(int playerIndex)
        {
            return _players[playerIndex - 1];
        }

        public bool Renta(int playerIndex, (string, FieldType, int, bool) k)
        {
            var z = GetPlayerInfo(playerIndex);
            Player? o = default;
            switch (k.Item2)
            {
                case FieldType.AUTO:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z.Cash -= 250;
                    o.Cash += 250;
                    break;

                case FieldType.FOOD:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z.Cash -= 250;
                    o.Cash += 250;
                    break;

                case FieldType.TRAVEL:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z.Cash -= 300;
                    o.Cash += 300;
                    break;

                case FieldType.CLOTHER:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z.Cash -= 100;
                    o.Cash += 1000;
                    break;

                case FieldType.PRISON:
                    z.Cash -= 1000;
                    break;

                case FieldType.BANK:
                    z.Cash -= 700;
                    break;

                default:
                    return false;
            }
             return true;
        }
    }
}
