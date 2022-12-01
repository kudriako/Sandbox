using System.Collections.Generic;
using System.Linq;

namespace MonopolyFoundation
{
    public class Monopoly
    {
        public List<(string, int)> players = new List<(string, int)>();
        public List<(string, Monopoly.Type, int, bool)> fields = new List<(string, Type, int, bool)>();

        public Monopoly(string[] p, int v)
        {
            for (int i = 0; i < v; i++)
            {
                players.Add((p[i], 6000));
            }
            fields.Add(("Ford", Monopoly.Type.AUTO, 0, false));
            fields.Add(("MCDonald", Monopoly.Type.FOOD, 0, false));
            fields.Add(("Lamoda", Monopoly.Type.CLOTHER, 0, false));
            fields.Add(("Air Baltic", Monopoly.Type.TRAVEL, 0, false));
            fields.Add(("Nordavia", Monopoly.Type.TRAVEL, 0, false));
            fields.Add(("Prison", Monopoly.Type.PRISON, 0, false));
            fields.Add(("MCDonald", Monopoly.Type.FOOD, 0, false));
            fields.Add(("TESLA", Monopoly.Type.AUTO, 0, false));
        }

        public List<(string, int)> GetPlayersList()
        {
            return players;
        }

        public enum Type
        {
            AUTO,
            FOOD,
            CLOTHER,
            TRAVEL,
            PRISON,
            BANK
        }

        public List<(string, Monopoly.Type, int, bool)> GetFieldsList()
        {
            return fields;
        }

        public (string, Type, int, bool) GetFieldByName(string v)
        {
            return (from p in fields where p.Item1 == v select p).FirstOrDefault();
        }

        public bool Buy(int v, (string, Type, int, bool) k)
        {
            var x = GetPlayerInfo(v);
            int cash = 0;
            switch (k.Item2)
            {
                case Type.AUTO:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 500;
                    players[v - 1] = (x.Item1, cash);
                    break;
                case Type.FOOD:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 250;
                    players[v - 1] = (x.Item1, cash);
                    break;
                case Type.TRAVEL:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 700;
                    players[v - 1] = (x.Item1, cash);
                    break;
                case Type.CLOTHER:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 100;
                    players[v - 1] = (x.Item1, cash);
                    break;
                default:
                    return false;
            }
            int i = players.Select((item, index) => new { name = item.Item1, index = index })
                .Where(n => n.name == x.Item1)
                .Select(p => p.index).FirstOrDefault();
            fields[i] = (k.Item1, k.Item2, v, k.Item4);
            return true;
        }

        public (string, int) GetPlayerInfo(int v)
        {
            return players[v - 1];
        }

        public bool Renta(int v, (string, Type, int, bool) k)
        {
            var z = GetPlayerInfo(v);
            (string, int) o = default;
            switch (k.Item2)
            {
                case Type.AUTO:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 250);
                    o = (o.Item1, o.Item2 + 250);
                    break;
                case Type.FOOD:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 250);
                    o = (o.Item1, o.Item2 + 250);

                    break;
                case Type.TRAVEL:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 300);
                    o = (o.Item1, o.Item2 + 300);
                    break;
                case Type.CLOTHER:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 100);
                    o = (o.Item1, o.Item2 + 1000);

                    break;
                case Type.PRISON:
                    z = (z.Item1, z.Item2 - 1000);
                    break;
                case Type.BANK:
                    z = (z.Item1, z.Item2 - 700);
                    break;
                default:
                    return false;
            }
            players[v - 1] = z;
            if (o != default)
                players[k.Item3 - 1] = o;
            return true;
        }
    }
}
