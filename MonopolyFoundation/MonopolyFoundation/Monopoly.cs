using System.Collections.Generic;
using System.Linq;

namespace MonopolyFoundation
{
    public class Monopoly
    {
        public List<(string, int)> players = new List<(string, int)>();
        public List<(string, FieldType, int, bool)> fields = new List<(string, FieldType, int, bool)>();

        public Monopoly(string[] p, int v)
        {
            for (int i = 0; i < v; i++)
            {
                players.Add((p[i], 6000));
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

        public List<(string, int)> GetPlayersList()
        {
            return players;
        }

        

        public List<(string, FieldType, int, bool)> GetFieldsList()
        {
            return fields;
        }

        public (string, FieldType, int, bool) GetFieldByName(string v)
        {
            return (from p in fields where p.Item1 == v select p).FirstOrDefault();
        }

        public bool Buy(int v, (string, FieldType, int, bool) k)
        {
            var x = GetPlayerInfo(v);
            int cash = 0;
            switch (k.Item2)
            {
                case FieldType.AUTO:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 500;
                    players[v - 1] = (x.Item1, cash);
                    break;
                case FieldType.FOOD:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 250;
                    players[v - 1] = (x.Item1, cash);
                    break;
                case FieldType.TRAVEL:
                    if (k.Item3 != 0)
                        return false;
                    cash = x.Item2 - 700;
                    players[v - 1] = (x.Item1, cash);
                    break;
                case FieldType.CLOTHER:
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

        public bool Renta(int v, (string, FieldType, int, bool) k)
        {
            var z = GetPlayerInfo(v);
            (string, int) o = default;
            switch (k.Item2)
            {
                case FieldType.AUTO:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 250);
                    o = (o.Item1, o.Item2 + 250);
                    break;
                case FieldType.FOOD:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 250);
                    o = (o.Item1, o.Item2 + 250);

                    break;
                case FieldType.TRAVEL:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 300);
                    o = (o.Item1, o.Item2 + 300);
                    break;
                case FieldType.CLOTHER:
                    if (k.Item3 == 0)
                        return false;
                    o = GetPlayerInfo(k.Item3);
                    z = (z.Item1, z.Item2 - 100);
                    o = (o.Item1, o.Item2 + 1000);

                    break;
                case FieldType.PRISON:
                    z = (z.Item1, z.Item2 - 1000);
                    break;
                case FieldType.BANK:
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
