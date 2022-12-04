using FluentAssertions;
using NUnit.Framework;

namespace MonopolyFoundation.Tests
{
    public class FieldTests
    {
        [Test]
        public void Field_WhenCreated_ReturnsNullOwner()
        {
            new Field("Field", FieldType.AUTO).Owner.Should().BeNull();
        }

        [Test]
        public void Field_WhenCreated_HasNoOwner()
        {
            new Field("Field", FieldType.AUTO).HasOwner().Should().BeFalse();
        }

        [Test]
        public void Field_WhenSetOwner_ReturnsPlayerAsOwner()
        {
            var player = new Player("Player", 1000);
            new Field("Field", FieldType.AUTO).SetOwner(player).Owner.Should().Be(player);
        }

        [Test]
        public void Field_WhenSetOwner_HasOwner()
        {
            var player = new Player("Player", 1000);
            new Field("Field", FieldType.AUTO).SetOwner(player).HasOwner().Should().BeTrue();
        }
    }
}
