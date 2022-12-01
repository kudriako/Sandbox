using FluentAssertions;
using NUnit.Framework;

namespace MonopolyFoundation.Tests
{
    public class MonopolyTests
    {
        [Test]
        public void GetPlayersListReturnCorrectList()
        {
            var players = new[] { "Peter", "Ekaterina", "Alexander" };
            var expectedPlayers = new[]
            {
                ("Peter",6000),
                ("Ekaterina",6000),
                ("Alexander",6000)
            };
            var monopoly = new Monopoly(players, 3);

            var actualPlayers = monopoly.GetPlayersList().ToArray();

            actualPlayers.Should().Equal(expectedPlayers);
        }

        [Test]
        public void GetFieldsListReturnCorrectList()
        {
            var expectedCompanies = new[]
            {
                ("Ford", FieldType.AUTO, 0, false),
                ("MCDonald", FieldType.FOOD, 0, false),
                ("Lamoda", FieldType.CLOTHER, 0, false),
                ("Air Baltic", FieldType.TRAVEL, 0, false),
                ("Nordavia", FieldType.TRAVEL, 0, false),
                ("Prison", FieldType.PRISON, 0, false),
                ("MCDonald", FieldType.FOOD, 0, false),
                ("TESLA", FieldType.AUTO, 0, false),
            };
            var players = new string[] { "Peter", "Ekaterina", "Alexander" };
            var monopoly = new Monopoly(players, 3);

            var actualCompanies = monopoly.GetFieldsList().ToArray();

            actualCompanies.Should().Equal(expectedCompanies);
        }

        [Test]
        public void PlayerBuyNoOwnedCompanies()
        {
            var players = new string[] { "Peter", "Ekaterina", "Alexander" };
            var monopoly = new Monopoly(players, 3);
            var x = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, x);

            var actualPlayer = monopoly.GetPlayerInfo(1);
            actualPlayer.Should().Be(("Peter", 5500));

            var actualField = monopoly.GetFieldByName("Ford");
            actualField.Item3.Should().Be(1);
        }

        [Test]
        public void RentaShouldBeCorrectTransferMoney()
        {
            var players = new string[] { "Peter", "Ekaterina", "Alexander" };
            var monopoly = new Monopoly(players, 3);
            var x = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, x);
            x = monopoly.GetFieldByName("Ford");
            monopoly.Renta(2, x);

            var player1 = monopoly.GetPlayerInfo(1);
            player1.Item2.Should().Be(5750);

            var player2 = monopoly.GetPlayerInfo(2);
            player2.Item2.Should().Be(5750);
        }
    }
}
