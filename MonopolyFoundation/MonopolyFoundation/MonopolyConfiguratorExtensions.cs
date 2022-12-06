namespace MonopolyFoundation
{
    public static class MonopolyConfiguratorExtensions
    {
        public static Monopoly SetUpPlayers(this Monopoly monopoly, string[] playerNames, int startingCash = 6000)
        {
            if (monopoly == null)
                throw new ArgumentNullException(nameof(monopoly));
            foreach (var name in playerNames)
            {
                monopoly.AddPlayer(new Player(name, startingCash));
            }
            return monopoly;
        }

        public static Monopoly SetUpFields(this Monopoly monopoly)
        {
            if (monopoly == null)
                throw new ArgumentNullException(nameof(monopoly));
            var factory = new FieldFactory();
            monopoly.AddField(factory.CreateField("Ford", FieldType.AUTO));
            monopoly.AddField(factory.CreateField("MCDonald", FieldType.FOOD));
            monopoly.AddField(factory.CreateField("Lamoda", FieldType.CLOTHER));
            monopoly.AddField(factory.CreateField("Air Baltic", FieldType.TRAVEL));
            monopoly.AddField(factory.CreateField("Nordavia", FieldType.TRAVEL));
            monopoly.AddField(factory.CreateField("Prison", FieldType.PRISON));
            monopoly.AddField(factory.CreateField("MCDonald", FieldType.FOOD));
            monopoly.AddField(factory.CreateField("TESLA", FieldType.AUTO));
            return monopoly;
        }
    }
}
