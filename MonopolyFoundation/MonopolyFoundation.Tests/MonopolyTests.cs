using FluentAssertions;
using NUnit.Framework;

using MonopolyFoundation.Fields;

namespace MonopolyFoundation.Tests
{
    public class MonopolyTests
    {
        [Test]
        public void Players_ReturnsSameReadOnlyList()
        {
            var monopoly = new Monopoly(new[] { "Peter", "Ekaterina", "Alexander" }, 3);
            monopoly.Players.Should().BeSameAs(monopoly.Players);
        }

        [Test]
        public void Fields_ReturnsSameReadOnlyList()
        {
            var monopoly = new Monopoly(new[] { "Peter", "Ekaterina", "Alexander" }, 3);
            monopoly.Fields.Should().BeSameAs(monopoly.Fields);
        }


        [Test]
        public void GetPlayersListReturnsCorrectList()
        {
            var players = new[] { "Peter", "Ekaterina", "Alexander" };
            var expectedPlayers = new[]
            {
                new Player("Peter", Monopoly.StartingCash),
                new Player("Ekaterina", Monopoly.StartingCash),
                new Player("Alexander", Monopoly.StartingCash),
            };
            var monopoly = new Monopoly(players, 3);

            var actualPlayers = monopoly.Players;

            actualPlayers.Should().Equal(expectedPlayers);
        }

        [Test]
        public void GetFieldsListReturnCorrectList()
        {
            var expectedCompanies = new FieldBase[]
            {
                new CommercialField("Ford", FieldType.AUTO),
                new CommercialField("MCDonald", FieldType.FOOD),
                new CommercialField("Lamoda", FieldType.CLOTHER),
                new CommercialField("Air Baltic", FieldType.TRAVEL),
                new CommercialField("Nordavia", FieldType.TRAVEL),
                new MunicipalField("Prison", FieldType.PRISON),
                new CommercialField("MCDonald", FieldType.FOOD),
                new CommercialField("TESLA", FieldType.AUTO),
            };
            var players = new string[] { "Peter", "Ekaterina", "Alexander" };
            var monopoly = new Monopoly(players, 3);

            var actualCompanies = monopoly.Fields;

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

            var actualPlayer = monopoly.GetPlayerByIndex(1);
            actualPlayer.Should().Be(new Player("Peter", cash - cost));

            var actualField = monopoly.GetFieldByName("Ford");
            actualField.Owner.Name.Should().Be("Peter");
        }

        [Test]
        public void RentaShouldBeCorrectTransferMoney()
        {
            var players = new string[] { "Peter", "Ekaterina", "Alexander" };
            var buyerIndex = 1;
            var ownerIndex = 2;
            var monopoly = new Monopoly(players, 3);

            var cash = Monopoly.StartingCash;
            var cost = 500;
            var renta = 250;

            var field1 = monopoly.GetFieldByName("Ford");
            monopoly.Buy(buyerIndex, field1);
            
            var field2 = monopoly.GetFieldByName("Ford");
            monopoly.Renta(ownerIndex, field2);

            var player1 = monopoly.GetPlayerByIndex(buyerIndex);
            player1.Cash.Should().Be(cash - cost + renta);

            var player2 = monopoly.GetPlayerByIndex(ownerIndex);
            player2.Cash.Should().Be(cash - renta);
        }
    }
}
