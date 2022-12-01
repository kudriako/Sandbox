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
                new Player("Peter",6000),
                new Player("Ekaterina",6000),
                new Player("Alexander",6000),
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
                new Field("Ford", FieldType.AUTO, 0, false),
                new Field("MCDonald", FieldType.FOOD, 0, false),
                new Field("Lamoda", FieldType.CLOTHER, 0, false),
                new Field("Air Baltic", FieldType.TRAVEL, 0, false),
                new Field("Nordavia", FieldType.TRAVEL, 0, false),
                new Field("Prison", FieldType.PRISON, 0, false),
                new Field("MCDonald", FieldType.FOOD, 0, false),
                new Field("TESLA", FieldType.AUTO, 0, false),
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

            var cash = 6000;
            var cost = 500;

            
            var field = monopoly.GetFieldByName("Ford");
            monopoly.Buy(1, field);

            var actualPlayer = monopoly.GetPlayerInfo(1);
            actualPlayer.Should().Be(new Player("Peter", cash - cost));

            var actualField = monopoly.GetFieldByName("Ford");
            actualField.OwnerIndex.Should().Be(1);
        }

        [Test]
        public void RentaShouldBeCorrectTransferMoney()
        {
            var players = new string[] { "Peter", "Ekaterina", "Alexander" };
            var buyerIndex = 1;
            var ownerIndex = 2;
            var monopoly = new Monopoly(players, 3);

            var cash = 6000;
            var cost = 500;
            var renta = 250;

            var field1 = monopoly.GetFieldByName("Ford");
            monopoly.Buy(buyerIndex, field1);
            
            var field2 = monopoly.GetFieldByName("Ford");
            monopoly.Renta(ownerIndex, field2);

            var player1 = monopoly.GetPlayerInfo(buyerIndex);
            player1.Cash.Should().Be(cash - cost + renta);

            var player2 = monopoly.GetPlayerInfo(ownerIndex);
            player2.Cash.Should().Be(cash - renta);
        }
    }
}
