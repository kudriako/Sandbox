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
            var monopoly = new Monopoly().SetUpPlayers(new [] { "Peter", "Ekaterina", "Alexander" });
            monopoly.Players.Should().BeSameAs(monopoly.Players);
        }

        [Test]
        public void Fields_ReturnsSameReadOnlyList()
        {
            var monopoly = new Monopoly().SetUpPlayers(new[] { "Peter", "Ekaterina", "Alexander" });
            monopoly.Fields.Should().BeSameAs(monopoly.Fields);
        }


        [Test]
        public void GetPlayersListReturnsCorrectList()
        {
            var cash = 4200;
            var expectedPlayers = new[]
            {
                new Player("Peter", cash),
                new Player("Ekaterina", cash),
                new Player("Alexander", cash),
            };
            var monopoly = new Monopoly().SetUpPlayers(new[] { "Peter", "Ekaterina", "Alexander" }, cash);

            var actualPlayers = monopoly.Players;

            actualPlayers.Should().Equal(expectedPlayers);
        }

        [Test]
        public void GetFieldsListReturnCorrectList()
        {
            var expectedCompanies = new FieldBase[]
            {
                new CommercialField("Ford", FieldType.AUTO) { Price = 500, Income = 250, Charge = 250 },
                new CommercialField("MCDonald", FieldType.FOOD) { Price = 250, Income = 250, Charge = 250 },
                new CommercialField("Lamoda", FieldType.CLOTHER) { Price = 100, Income = 1000, Charge = 100 },
                new CommercialField("Air Baltic", FieldType.TRAVEL) { Price = 700, Income = 300, Charge = 300 },
                new CommercialField("Nordavia", FieldType.TRAVEL) { Price = 700, Income = 300, Charge = 300 },
                new MunicipalField("Prison", FieldType.PRISON) { Charge = 1000 },
                new CommercialField("MCDonald", FieldType.FOOD) { Price = 250, Income = 250, Charge = 250 },
                new CommercialField("TESLA", FieldType.AUTO) { Price = 500, Income = 250, Charge = 250 },
            };
            var monopoly = new Monopoly().SetUpFields();

            var actualCompanies = monopoly.Fields;

            actualCompanies.Should().Equal(expectedCompanies);
        }

        [Test]
        public void PlayerBuyNoOwnedCompanies()
        {
            var cash = 5000;
            var monopoly = new Monopoly().SetUpFields().SetUpPlayers(new [] { "Peter", "Ekaterina", "Alexander" }, cash);

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
            var cash = 7000;
            var cost = 500;
            var renta = 250;

            var buyerIndex = 1;
            var visitorIndex = 2;
            var monopoly = new Monopoly().SetUpFields().SetUpPlayers(new[] { "Peter", "Ekaterina", "Alexander" }, cash);

            var field1 = monopoly.GetFieldByName("Ford");
            monopoly.Buy(buyerIndex, field1);
            
            var field2 = monopoly.GetFieldByName("Ford");
            monopoly.Renta(visitorIndex, field2);

            var player1 = monopoly.GetPlayerByIndex(buyerIndex);
            player1.Cash.Should().Be(cash - cost + renta);

            var player2 = monopoly.GetPlayerByIndex(visitorIndex);
            player2.Cash.Should().Be(cash - renta);
        }
    }
}
