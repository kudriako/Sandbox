using FluentAssertions;
using NUnit.Framework;

namespace PolishNotation.Tests
{
    [TestFixture]
    public class StringExpressionsTests
    {
        private const double Epsilon = 1E-8d;

        [Test]
        [TestCase(null, default(double))]
        [TestCase("", default(double))]
        [TestCase(" ", default(double))]
        [TestCase("   ", default(double))]
        public void Evaluate_WhenInvalidValue_ReturnsDefaultValue(string text, double result)
        {
            text.Evaluate().Should().BeApproximately(result, Epsilon);
        }

        [Test]
        [TestCase("0", 0d)]
        [TestCase("42", 42d)]
        [TestCase("0.9", 0.9d)]
        [TestCase(".123", 0.123d)]
        [TestCase("-78.1", -78.1d)]
        public void Evaluate_WhenSingleValue_ReturnsParsedValue(string text, double result)
        {
            text.Evaluate().Should().BeApproximately(result, Epsilon);
        }

        [Test]
        [TestCase("+ 43.2 12.0", 55.2)]
        [TestCase("- 43.2 12.0", 31.2)]
        [TestCase("* 43.2 12.0", 518.4)]
        [TestCase("/ 43.2 12.0", 3.6)]
        public void Evaluate_WhenOperation_ReturnsResult(string text, double result)
        {
            text.Evaluate().Should().BeApproximately(result, Epsilon);
        }
    }
}