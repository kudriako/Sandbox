using FluentAssertions;
using NUnit.Framework;

namespace PolishNotation.Tests
{
    [TestFixture]
    public class StringExpressionsTests
    {
        [Test]
        [TestCase(null, default(double))]
        [TestCase("", default(double))]
        [TestCase(" ", default(double))]
        [TestCase("   ", default(double))]
        public void Evaluate_WhenInvalidValue_ReturnsDefaultValue(string text, double result)
        {
            text.Evaluate().Should().BeApproximately(result, double.Epsilon);
        }
    }
}